#version 330
layout(location=0) in vec3 vertexPosition_modelspace;
layout(location=1) in vec3 vertexColor;
uniform mat4 MVP;
out vec3 vColor;

void main() {
  gl_Position = MVP * vec4(vertexPosition_modelspace, 1.0);
  vColor = vertexColor; //vec4(0.5, 0.0, 0.0, 1.0);
}