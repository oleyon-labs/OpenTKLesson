#version 330
//uniform vec4 ourColor;

in vec4 vertexColor;
out vec4 fragColor;

void main() {
  fragColor = vertexColor;//vec4(1.0f,0.0f,0.0f,0.0f);//vertexColor;//ourColor;
}