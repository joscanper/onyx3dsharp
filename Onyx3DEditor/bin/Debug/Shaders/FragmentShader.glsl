#version 330

in vec3 o_color;
in vec3 o_normal;
in vec3 o_fragpos;

out vec4 fragColor;

//vec3 lightpos = vec3(3,3,3);

void main()
{
    fragColor = vec4(o_color,1);
}
