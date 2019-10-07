using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Graphic;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Scene.Widget
{
    public class ImageWidget : Widget
    {
        private readonly VertexArrayObj _vao = VertexArrayObj.Create();
        private readonly VertexBuffer _buffer = VertexBuffer.Create(BufferTarget.ArrayBuffer);

        private float[] BufferData =
        {
            -0.9f, -0.9f, 0.0f, 1.0f,
            0.85f, -0.9f, 1.0f, 1.0f,
            -0.9f, 0.85f, 0.0f, 0.0f,
            0.9f, -0.85f, 1.0f, 1.0f,
            0.9f, 0.9f, 1.0f, 0.0f,
            -0.85f, 0.9f, 0.0f, 0.0f
        };

        public Texture image;

        public ImageWidget(Widget parent) : base(parent)
        {
            UIShader.Use();
            _buffer.BufferData(BufferData);
            _vao.SetAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            _vao.SetAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            _vao.SetVertexAttribArrayEnable(0);
            _vao.SetVertexAttribArrayEnable(1);
        }

        public override void Refresh()
        {
            
        }

        public override void Draw()
        {
            image.Bind();

            _vao.Bind();
            _buffer.Bind();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
