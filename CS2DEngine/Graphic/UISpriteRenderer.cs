using System.Drawing;
using CS2DEngine.Content;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Graphic
{
    public class UISpriteRenderer
    {
        private static readonly VertexArrayObj Vao = VertexArrayObj.Create();
        private static readonly VertexBuffer Buffer = VertexBuffer.Create(BufferTarget.ArrayBuffer);

        private static readonly ShaderProgram Shader
            = ContentManager.Load<ShaderProgram, ShaderProgramReader>("[CS2DEngine]CS2DEngine.Shader.UI.shader");

        private static readonly float[] BufferData = 
        {
            -1.0f, -1.0f, 0.0f, 1.0f,
            1.0f, -1.0f, 1.0f, 1.0f,
            -1.0f, 1.0f, 0.0f, 0.0f,
            1.0f, -1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 0.0f,
            -1.0f, 1.0f, 0.0f, 0.0f
        };

        static UISpriteRenderer()
        {
            Buffer.BufferData(BufferData);
            Vao.SetAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            Vao.SetAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            Vao.SetVertexAttribArrayEnable(0);
            Vao.SetVertexAttribArrayEnable(1);
        }

        public Texture texture;
        public float zOrder;
        public Color color = Color.White;

        private Matrix4 GetTransformMatrix()
        {
            var matrix = Matrix4.Identity;
            matrix *= Matrix4.CreateTranslation(0, 0, zOrder);

            return matrix;
        }

        public void Draw()
        {
            if (texture == null)
                return;

            texture.Bind();
            Vao.Bind();
            Buffer.Bind();
            Shader.Use();
            Shader.SetUniform4("color", new Vector4(color.R, color.G, color.B, color.A) / 255.0f);
            Shader.SetUniformMatrix4("transform", GetTransformMatrix());

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
