#version 330

in vec4 vertexColor;
in vec2 texCoord;
out vec4 color;

uniform sampler2D ourTexture1;
uniform sampler2D ourTexture2;
uniform vec4 outerColor;

void main() {
  color = /*vertexColor * */mix(texture(ourTexture1, texCoord), texture(ourTexture2, texCoord), 0.5);
}