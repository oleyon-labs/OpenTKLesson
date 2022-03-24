#version 330

layout(location=0) in vec3 vPosition;
layout(location=1) in vec3 color;
layout(location=2) in vec2 aTexCoord;

uniform mat4 model;

out vec4 vertexColor;
out vec2 texCoord;

out vec4 coord;

void main() {
  texCoord = aTexCoord;
  coord=vec4(vPosition.x*vPosition.x*vPosition.x, vPosition.y*vPosition.y*vPosition.y, vPosition.z, 1.0);
  gl_Position = model * vec4(vPosition.x*vPosition.x*vPosition.x*4, vPosition.y*vPosition.y*vPosition.y*4, vPosition.z, 1.0);
  vertexColor = vec4(color, 1.0);

}