using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Graphic
{
    public class VertexBuffer
    {
        private readonly int _bufferId;
        private readonly BufferTarget _target;

        private VertexBuffer(BufferTarget target, int bufferId)
        {
            _bufferId = bufferId;
            _target = target;
        }

        public static VertexBuffer Create(BufferTarget target)
        {
            return new VertexBuffer(target, GL.GenBuffer());
        }

        public void Bind()
        {
            GL.BindBuffer(_target, _bufferId);
        }

        public void BufferData(float[] data)
        {
            GL.BindBuffer(_target, _bufferId);
            GL.BufferData(_target, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);
        }

        ~VertexBuffer()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!Engine.IsExiting)
                GL.DeleteBuffer(_bufferId);

            GC.SuppressFinalize(this);
        }
    }
}
