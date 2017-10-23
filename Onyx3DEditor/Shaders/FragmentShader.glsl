#version 330

in vec3 o_color;

out vec4 fragColor;

void main()
{
    fragColor = vec4(o_color, 1.0);
}
