#version 330

in vec3 o_normal;
in vec3 o_fragpos;

out vec4 fragColor;

uniform vec4 color;
uniform samplerCube cubemap;

// ------------------------------- Camera UBO

layout(std140) uniform CameraData 
{
	mat4 V; 
	mat4 P; 
	vec4 cameraPos; 
};

void main()
{ 
	
	vec3 I = normalize(cameraPos.xyz-o_fragpos);
	//vec3 R = reflect(I, N);
	vec3 coord = o_normal * vec3(1,-1,-1);
	fragColor = vec4(texture(cubemap, coord).rgb, 1.0f);
}
