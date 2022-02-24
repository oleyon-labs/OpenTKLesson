#version 330

layout(location=0) in vec3 vPosition;

out vec4 vertexColor;

void main() {
  gl_Position = vec4(vPosition, 1.0);
  vertexColor = vec4(vPosition, 1.0);
}