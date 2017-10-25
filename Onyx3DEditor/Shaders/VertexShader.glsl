#version 330

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec3 normal;

//uniform mat4 MVP;
uniform mat4 M;
uniform mat4 V;
uniform mat4 P;

out vec3 o_color;
out vec3 o_normal;
out vec3 o_fragpos;

void main()
{
	o_color = color;
	o_normal = (M * vec4(normal,1)).xyz;
	vec4 worldPos = M * vec4(position, 1);
	o_fragpos = worldPos.xyz;
	gl_Position = P * V * worldPos;
}
