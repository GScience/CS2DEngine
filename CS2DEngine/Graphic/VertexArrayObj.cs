using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Graphic
{
    public class VertexArrayObj
    {
        private readonly int _vaoId;

        private VertexArrayObj(int vaoId)
        {
            _vaoId = vaoId;
        }

        public static VertexArrayObj Create()
        {
            return new VertexArrayObj(GL.GenVertexArray());
        }

        public void Bind()
        {
            GL.BindVertexArray(_vaoId);
        }

        public void SetVertexAttribArrayEnable(int pos)
        {
            GL.EnableVertexAttribArray(pos);
        }

        public void SetVertexAttribArrayDisable(int pos)
        {
            GL.DisableVertexAttribArray(pos);
        }

        public void SetAttribPointer(
            int index, 
            int size, 
            VertexAttribPointerType type, 
            bool normalized, 
            int stride,
            int offset)
        {
            Bind();
            GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
        }

        ~VertexArrayObj()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!Engine.IsExiting)
                GL.DeleteVertexArray(_vaoId);
            GC.SuppressFinalize(this);
        }
    }
}
