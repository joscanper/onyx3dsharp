#version 330

#define MAX_DIR_LIGHTS 2
#define MAX_POINT_LIGHTS 8
#define MAX_SPOT_LIGHTS 8

const float PI = 3.14159265359f;

in vec3 o_color;
in vec3 o_normal;
in vec3 o_fragpos;
in vec2 o_uv;

out vec4 fragColor;

vec3 lightdir = vec3(0,1,2);

uniform sampler2D base_texture;
uniform vec4 base_color;
uniform float fresnel;
uniform float fresnel_strength;

float metallic = 0.5f;
float roughness = 0.15f;
float ao = 0.0f;

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
	float range;
	float linear;
	float quadratic;
};

layout(std140) uniform LightingData { 
	vec3 ambient;
    
    int pointLightsNum;
    Light pointLight[MAX_POINT_LIGHTS];
    
    int dirLightsNum;
    Light dirLight[MAX_DIR_LIGHTS];
    
    int spotLightsNum;
    Light spotLight[MAX_SPOT_LIGHTS];
};

// ----------------------------------

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

void main()
{		
	vec3 albedo = (texture(base_texture, o_uv) * base_color).rgb;
	vec3 WorldPos = o_fragpos;
    vec3 N = normalize(o_normal);
    vec3 V = normalize(cameraPos.xyz - WorldPos);

    vec3 F0 = vec3(0.1); 
    F0 = mix(F0, albedo, metallic);
	           
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
        vec3 radiance     = pointLight[i].color.rgb * attenuation;        
        
        // cook-torrance brdf
        float NDF = DistributionGGX(N, H, roughness);        
        float G   = GeometrySmith(N, V, L, roughness);      
        vec3 F    = fresnelSchlick(max(dot(H, V), 0.0), F0);       
        
        vec3 kS = F;
        vec3 kD = vec3(1.0) - kS;
        kD *= 1.0 - metallic;	  
        
        vec3 numerator    = NDF * G * F;
        float denominator = 4.0 * max(dot(N, V), 0.0) * max(dot(N, L), 0.0);
        vec3 specular     = numerator / max(denominator, 0.001);  
		
        // add to outgoing radiance Lo
        float NdotL = max(dot(N, L), 0.0);                
        Lo += (kD * albedo / PI + specular) * radiance * NdotL; 
    }   
  
    vec3 ambient = vec3(0.5) * albedo * ao;
    vec3 color = ambient + Lo;
	
    color = color / (color + vec3(1.0));
    color = pow(color, vec3(1.0/2.2));  
   
    fragColor = vec4(color, 1.0);
}  
