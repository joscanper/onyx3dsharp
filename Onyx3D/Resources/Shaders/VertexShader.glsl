#version 330

#define MAX_DIR_LIGHTS 2
#define MAX_POINT_LIGHTS 8
#define MAX_SPOT_LIGHTS 8

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec3 normal;
layout (location = 3) in vec2 texcoord;

//uniform mat4 MVP;
uniform mat4 M;
uniform mat4 R;

out vec3 o_color;
out vec3 o_normal;
out vec3 o_fragpos;
out vec2 o_uv;

//#include OnyxShaderCamera
//#include OnyxShaderLighting
//#include OnyxShaderUtils

layout(std140) uniform CameraData { 
	mat4 V; 
	mat4 P; 
	vec4 cameraPos; 
};


// ------------------------------- Lighting UBO
/*
struct Light{
    float range;
    vec3 position;
    vec3 direction;
    vec3 color;
    float angle;
    vec3 specular;
    float intensity;
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

*/
void main()
{
	o_color = color;
	o_uv = vec2(texcoord.x, 1.0 - texcoord.y);
	
	//o_normal = normalize((M * vec4(normal, 0.0f)).xyz);
	o_normal = normalize(mat3(transpose(inverse(M))) * normal); 
	vec4 worldPos = M * vec4(position, 1.0f);
	o_fragpos = worldPos.xyz;
	gl_Position = P * V * worldPos;
}
