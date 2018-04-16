#version 330

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec3 normal;
layout (location = 3) in vec2 texcoord;

uniform mat4 NM;
uniform mat4 M;

out vec3 o_fragpos;
out vec3 o_normal;
out vec2 o_uv;

layout(std140) uniform CameraData { 
	mat4 V; 
	mat4 P; 
	vec4 cameraPos; 
};

void main()
{
	
	o_uv = vec2(texcoord.x, 1.0 - texcoord.y); 

	vec4 worldPos = M * vec4(position, 1.0f);
	o_fragpos = worldPos.xyz;
	gl_Position = P * mat4(mat3(V)) * worldPos;
}
