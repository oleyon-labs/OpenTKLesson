#version 330

in vec4 vertexColor;
in vec2 texCoord;
out vec4 color;

uniform sampler2D ourTexture1;
uniform sampler2D ourTexture2;
uniform vec4 outerColor;

void main() {
	vec4 textureColor1=texture(ourTexture1, texCoord);
	vec4 textureColor2=texture(ourTexture2, texCoord);

	color = 1 - abs(textureColor1 - textureColor2);
	//color.r = abs(textureColor1.g - textureColor2.b);
	//color.g = abs(textureColor1.r - textureColor2.b);
	//color.b = abs(textureColor1.r - textureColor2.g);
	//color = /*vertexColor * */mix(textureColor1, textureColor2, 0.5);
}