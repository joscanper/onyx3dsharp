#version 330

in vec3 o_normal;
in vec3 o_fragpos;

out vec4 fragColor;

uniform vec4 color;
uniform samplerCube cubemap;

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

// ------------------------------- Camera UBO

layout(std140) uniform CameraData 
{
	mat4 V; 
	mat4 P; 
	vec4 cameraPos; 
};

void main()
{ 
	
	vec3 I = normalize(o_fragpos - cameraPos.xyz);
	vec3 R = reflect(I, normalize(o_normal));
	vec3 coord = R * vec3(1,-1,-1);
	
	
	// Irradiance map
	/*
	vec3 R = o_normal;
	vec3 coord = R * vec3(1,-1,-1);
	
	coord.x += rand(o_normal.xy) -0.5f;
	coord.z += rand(o_normal.yz) -0.5f;
	coord.y += rand(o_normal.zx) -0.5f;
	*/
	
	
	fragColor = vec4(texture(cubemap, coord).rgb, 1.0f);
}
