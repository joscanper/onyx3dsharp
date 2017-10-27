#version 330

in vec3 o_color;
in vec3 o_normal;
in vec3 o_fragpos;

out vec4 fragColor;

vec3 lightdir = vec3(0,1,2);

//#include OnyxShaderCamera

layout(std140) uniform CameraData { 
	mat4 V; 
	mat4 P; 
	vec3 cameraPos; 
};


void main()
{ 
	vec3 dirToCam = cameraPos - o_fragpos;
	vec3 N = normalize(o_normal);

	float nDotL = dot(N, normalize(lightdir));
	float fresnel = abs(pow(1-dot(N, dirToCam),2));
	float col = clamp(nDotL + fresnel,0,1);
	fragColor = vec4(vec3(o_color*col),1);
}
