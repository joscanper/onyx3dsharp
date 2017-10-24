#version 330

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;

uniform mat4 MVP;
uniform mat4 M;
uniform mat4 V;
uniform mat4 P;

out vec3 o_color;

void main()
{
	o_color = color;
    gl_Position = P * V * M * vec4(position, 1);
}
