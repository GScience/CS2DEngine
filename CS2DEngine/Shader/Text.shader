#version 330

VertexShader
{
	layout(location = 0) in vec4 vPosition;
	layout(location = 1) in vec4 vTexCoord;

	uniform mat4 transform;

	out vec2 texCoord;

	void main()
	{
		vec4 texCoord4 = vTexCoord * transform;
		texCoord = vec2(texCoord4.x, texCoord4.y);
		gl_Position = vPosition * transform;
	}
}

FragmentShader
{
	uniform sampler2D tex;
	uniform vec4 color;

	out vec4 fColor;

	in vec2 texCoord;

	void main()
	{
		fColor = texture(tex, texCoord);
		fColor *= color;
	}
}