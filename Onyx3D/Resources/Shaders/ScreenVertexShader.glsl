#version 330

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 color;
layout (location = 2) in vec3 normal;
layout (location = 3) in vec2 texcoord;

out vec3 o_normal;
out vec2 o_uv;

void main()
{
	o_uv = texcoord;
	gl_Position = vec4((texcoord.x - 0.5f) * 2, (texcoord.y -0.5f) * 2, 0, 1);
}
