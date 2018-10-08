#version 330

#define MAX_DIR_LIGHTS 2
#define MAX_POINT_LIGHTS 8
#define MAX_SPOT_LIGHTS 8

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec3 normal;
layout (location = 3) in vec2 texcoord;
layout (location = 4) in vec3 tangent;
layout (location = 5) in vec3 bitangent;

uniform mat4 NM;
uniform mat4 M;

out vec3 o_color;
out vec3 o_normal;
out vec3 o_fragpos;
out vec2 o_uv;
out mat3 o_tbn;
//out vec3 o_tangent_fragpos;
//out vec3 o_tangent_campos;


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

	
	vec3 o_tangent = normalize(tangent);
	vec3 o_bitangent = normalize(bitangent);
	vec3 o_normal = normalize(normal);
	
	o_color = color;
	o_uv = vec2(texcoord.x, 1.0 - texcoord.y);

	o_normal = cross(o_tangent, o_bitangent);
	o_tangent = -cross(o_normal, normalize(bitangent));
	o_bitangent = -cross(o_normal, normalize(tangent));
	o_normal *= sign(dot(o_normal , normal));
	
	o_tangent    	= normalize(vec3(NM * vec4(o_tangent,   0.0)));
	o_bitangent 	= normalize(vec3(NM * vec4(o_bitangent, 0.0)));
	o_normal   		= normalize(vec3(NM * vec4(o_normal,   0.0f)));

	o_tbn = mat3(o_tangent, o_bitangent, o_normal);
	
	
	//o_normal = normalize(cross(o_tangent, o_bitangent)); 

	vec4 worldPos = M * vec4(position, 1.0f);
	o_fragpos = worldPos.xyz;
	gl_Position = P * V * worldPos;
}
