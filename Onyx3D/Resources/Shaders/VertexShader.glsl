#version 330

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec3 normal;
layout (location = 3) in vec2 texcoord;

//uniform mat4 MVP;
uniform mat4 M;

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
	vec3 cameraPos; 
};

void main()
{
	o_color = color;
	o_uv = vec2(texcoord.x, 1.0 - texcoord.y);
	
	o_normal = (M * vec4(normal,1)).xyz;
	vec4 worldPos = M * vec4(position, 1);
	o_fragpos = worldPos.xyz;
	gl_Position = P * V * worldPos;
}
