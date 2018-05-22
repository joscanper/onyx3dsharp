#version 330

#define IRRADIANCE_LOD 4
#define REFLECTION_LOD 10

#define MAX_DIR_LIGHTS 2
#define MAX_POINT_LIGHTS 8
#define MAX_SPOT_LIGHTS 8

const float PI = 3.14159265359f;

in vec3 o_color;
in vec3 o_normal;
in vec3 o_fragpos;
in vec2 o_uv;
in mat3 o_tbn;

out vec4 fragColor;

vec3 lightdir = vec3(0,1,2);

uniform sampler2D albedo;
uniform sampler2D metallic;
uniform sampler2D roughness;
uniform sampler2D occlusion;
uniform sampler2D normal;
uniform samplerCube environment_map;
uniform sampler2D brdfLUT;

uniform vec4 base_color;
uniform float metallic_strength;
uniform float roughness_strength;
uniform float occlusion_strength;


// ------------------------------- Camera UBO

layout(std140) uniform CameraData { 
	mat4 V; 
	mat4 P; 
	vec4 cameraPos; 
};

// ------------------------------- Lighting UBO

struct Light {
	vec4 position;
	vec4 direction;
    vec4 color;
    vec4 specular;
	float angle;
	float intensity;

	float linear;
	float quadratic;
};

layout(std140) uniform LightingData { 
	vec4 ambient;
    
    Light pointLight[MAX_POINT_LIGHTS];
    Light dirLight[MAX_DIR_LIGHTS];
    Light spotLight[MAX_SPOT_LIGHTS];

	int pointLightsNum;
    int dirLightsNum;
    int spotLightsNum;
};

// ----------------------------------

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float DistributionGGX(vec3 N, vec3 H, float roughness)
{
    float a      = roughness*roughness;
    float a2     = a*a;
    float NdotH  = max(dot(N, H), 0.0);
    float NdotH2 = NdotH*NdotH;
	
    float num   = a2;
    float denom = (NdotH2 * (a2 - 1.0) + 1.0);
    denom = PI * denom * denom;
	
    return num / denom;
}

float GeometrySchlickGGX(float NdotV, float roughness)
{
    float r = (roughness + 1.0);
    float k = (r*r) / 8.0;

    float num   = NdotV;
    float denom = NdotV * (1.0 - k) + k;
	
    return num / denom;
}
float GeometrySmith(vec3 N, vec3 V, vec3 L, float roughness)
{
    float NdotV = max(dot(N, V), 0.0);
    float NdotL = max(dot(N, L), 0.0);
    float ggx2  = GeometrySchlickGGX(NdotV, roughness);
    float ggx1  = GeometrySchlickGGX(NdotL, roughness);
	
    return ggx1 * ggx2;
}

vec3 fresnelSchlick(float cosTheta, vec3 F0)
{
    return F0 + (1.0 - F0) * pow(1.0 - cosTheta, 5.0);
}  

vec3 fresnelSchlickRoughness(float cosTheta, vec3 F0, float roughness)
{
    return F0 + (max(vec3(1.0 - roughness), F0) - F0) * pow(1.0 - cosTheta, 5.0);
}   


void main()
{		
	vec3 albedo_color = (texture(albedo, o_uv) * base_color).rgb;
	
	float metallic_f = texture(metallic, o_uv).r * metallic_strength;
	float roughness_f = max(texture(roughness, o_uv).r * roughness_strength, 0.001f);
	float ao_f = texture(occlusion, o_uv).r * occlusion_strength;

	vec3 WorldPos = o_fragpos;
    vec3 N = normalize(texture(normal, o_uv).rgb * 2.0 - 1.0);
	N = normalize(o_tbn * N);
    vec3 V = normalize(cameraPos.xyz - WorldPos);

    vec3 F0 = vec3(0.1); 
    F0 = mix(F0, albedo_color, metallic_f);
	           
    // reflectance equation
    vec3 Lo = vec3(0.0);
    for(int i = 0; i < pointLightsNum; ++i) 
    {
        // calculate per-light radiance
		vec3 lightPos = pointLight[i].position.xyz;
        vec3 L = normalize(lightPos - WorldPos);
        vec3 H = normalize(V + L);
		
        float distance    = length(lightPos - WorldPos);
        float attenuation = 1.0 / (distance * distance);
        vec3 radiance     = pointLight[i].color.rgb * attenuation * pointLight[i].intensity;
        
        // cook-torrance brdf
        float NDF = DistributionGGX(N, H, roughness_f);        
        float G   = GeometrySmith(N, V, L, roughness_f);      
        vec3 F    = fresnelSchlick(max(dot(H, V), 0.0), F0);       
        
        vec3 kS = F;
        vec3 kD = 1.0 - kS;
        kD *= 1.0 - metallic_f;	  
 
        vec3 numerator    = NDF * G * F;
        float denominator = 4.0 * max(dot(N, V), 0.0) * max(dot(N, L), 0.0);
        vec3 specular     =  numerator / max(denominator, 0.001);  
		
        // add to outgoing radiance Lo
        float NdotL = max(dot(N, L), 0.0);                
        Lo += (kD * albedo_color / PI + specular) * radiance * NdotL; 
    }   

	vec3 R = reflect(-V, N);
    vec3 F = fresnelSchlickRoughness(max(dot(N, V), 0.0), F0, roughness_f);
    vec2 envBRDF  = texture(brdfLUT, vec2(max(dot(N, V), 0.0), roughness_f)).rg;

    vec3 kS = F;
    vec3 kD = 1.0 - kS;
    kD *= 1.0 - metallic_f;

	vec3 reflection = textureLod(environment_map, R * vec3(1,-1,-1), roughness_f * REFLECTION_LOD).rgb;
	vec3 specular = reflection * (F * envBRDF.x + envBRDF.y);

    vec3 irradiance = textureLod(environment_map,  N * vec3(1,-1,-1), IRRADIANCE_LOD).rgb;
    vec3 diffuse      = irradiance * albedo_color;
    vec3 ambient_color = (kD * diffuse + specular) * ao_f;
    
    vec3 color = ambient_color + Lo;
	color = color / (color + vec3(1.0));
    color = pow(color, vec3(1.0/2.2));  
   
    fragColor = vec4(color, 1.0);
}  