#version 330

VertexShader
{
	layout(location = 0) in vec4 vPosition;
	layout(location = 1) in vec2 vTexCoord;

	uniform mat4 transform;

	out vec2 texCoord;

	void main()
	{
		texCoord = vTexCoord;
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