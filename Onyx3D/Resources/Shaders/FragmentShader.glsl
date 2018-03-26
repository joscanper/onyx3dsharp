#version 330

#define MAX_DIR_LIGHTS 2
#define MAX_POINT_LIGHTS 8
#define MAX_SPOT_LIGHTS 8

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

//#include OnyxShaderCamera

// ------------------------------- Camera UBO

layout(std140) uniform CameraData { 
	mat4 V; 
	mat4 P; 
	vec3 cameraPos; 
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

// -------------------------------

float calculateLighting(vec3 normal){
	float ret = 1.0f;
	/*for (int i = 0; i < pointLightsNum; ++i)
	{
		ret *= dot(normal, normalize(pointLight[i].position));
	}*/
	return dot(normal, normalize(pointLight[0].position.xyz));
}

void main()
{ 
	
	vec3 dirToCam = normalize(cameraPos - o_fragpos);
	vec3 N = normalize(o_normal);

	vec4 t = texture(base_texture, o_uv) * base_color;

	float nDotL = calculateLighting(N);
	//float nDotL = 1;


	//float rim = clamp(abs(pow(1-dot(N, dirToCam), fresnel)) * fresnel_strength,0,1);
	//float col = clamp(nDotL + rim,0,1);
	fragColor = vec4(vec3(t*nDotL),1);
}
