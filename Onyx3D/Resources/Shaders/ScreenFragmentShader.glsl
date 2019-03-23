#version 330

in vec3 o_color;
in vec3 o_normal;
in vec2 o_uv;

out vec4 fragColor;

uniform sampler2D maintex;

void main()
{		

	vec4 col = texture(maintex, o_uv);
    fragColor = col;
}  