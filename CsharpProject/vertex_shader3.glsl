#version 330

layout(location=0) in vec3 vPosition;
layout(location=1) in vec3 color;
layout(location=2) in vec2 aTexCoord;

uniform mat4 model;
out vec4 vertexColor;
out vec2 texCoord;

void main() {
  texCoord = aTexCoord;
  gl_Position = model * vec4(vPosition, 1.0);
  vertexColor = vec4(color, 1.0);
}