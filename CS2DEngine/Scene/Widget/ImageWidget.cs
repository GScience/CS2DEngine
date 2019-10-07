using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Graphic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Scene.Widget
{
    public class ImageWidget : Widget
    {
        private readonly VertexArrayObj _vao = VertexArrayObj.Create();
        private readonly VertexBuffer _buffer = VertexBuffer.Create(BufferTarget.ArrayBuffer);

        private float[] BufferData;

        public float zOrder = 0.2f;
        public Texture image;
        public Color color = Color.White;

        public ImageWidget(Widget parent) : base(parent)
        {

        }

        public override void Refresh()
        {
            BufferData = new []
            {
                -0.9f, -0.9f, zOrder, 0.0f, 1.0f,
                0.85f, -0.9f, zOrder, 1.0f, 1.0f,
                -0.9f, 0.85f, zOrder, 0.0f, 0.0f,
                0.9f, -0.85f, zOrder, 1.0f, 1.0f,
                0.9f, 0.9f, zOrder, 1.0f, 0.0f,
                -0.85f, 0.9f, zOrder, 0.0f, 0.0f
            };

            UIShader.Use();
            _buffer.BufferData(BufferData);
            _vao.SetAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            _vao.SetAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            _vao.SetVertexAttribArrayEnable(0);
            _vao.SetVertexAttribArrayEnable(1);
        }

        public override void Draw()
        {
            UIShader.SetUniform4("color", new Vector4(color.R, color.G, color.B, color.A) / 255.0f);

            if (image == null)
                Texture.Unbind();
            else
                image.Bind();

            _vao.Bind();
            _buffer.Bind();
            UIShader.Use();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
