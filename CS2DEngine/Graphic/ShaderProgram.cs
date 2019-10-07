using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Graphic
{
    public class ShaderProgram
    {
        private readonly int _programId;

        private ShaderProgram(int programId)
        {
            _programId = programId;
        }

        public static ShaderProgram Create()
        {
            var programId = GL.CreateProgram();
            return new ShaderProgram(programId);
        }

        public void Use()
        {
            GL.UseProgram(_programId);
        }

        public int GetProgramId()
        {
            return _programId;
        }
    }
}
