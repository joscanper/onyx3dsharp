#version 330

in vec3 o_color;
in vec3 o_normal;
in vec3 o_fragpos;
in vec2 o_uv;

out vec4 fragColor;

vec3 lightdir = vec3(0,1,2);

uniform sampler2D base_texture;
uniform vec4 base_color;
uniform float fresnel;
uniform float fresnel_strength;

//#include OnyxShaderCamera

layout(std140) uniform CameraData { 
	mat4 V; 
	mat4 P; 
	vec3 cameraPos; 
};


void main()
{ 
	
	vec3 dirToCam = normalize(cameraPos - o_fragpos);
	vec3 N = normalize(o_normal);

	vec4 t = texture(base_texture, o_uv) * base_color;

	float nDotL = dot(N, normalize(lightdir));
	float rim = clamp(abs(pow(1-dot(N, dirToCam), fresnel)) * fresnel_strength,0,1);
	float col = clamp(nDotL + rim,0,1);
	fragColor = vec4(vec3(t*nDotL+rim),1);
}
