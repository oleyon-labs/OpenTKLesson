#version 330
layout(location=0) in vec3 aPos;
layout(location=1) in vec3 aCol;
uniform mat4 model;
out vec4 vertexColor;

void main() {
  gl_Position = model * vec4(aPos, 1.0);
  vertexColor = vec4(aCol,1.0f); //vec4(0.5, 0.0, 0.0, 1.0);
}