#version 330 core
//layout (location = 0) in vec3 aPosition; // vertex coordinates
//layout (location = 1) in vec2 aTexCoord; // texture coordinates
//layout (location = 2) in vec3 aNormal;


in vec3 vPosition;
out vec3 glPosition;

void main (void)
{
	gl_Position = vec4(vPosition, 1.0);
	glPosition = vPosition;
}