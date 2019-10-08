using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Graphic
{
    public class ShaderProgram
    {
        private readonly int _programId;
        private Dictionary<string, int> _uniformLocDictionary = new Dictionary<string, int>();

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

        public void SetUniform4(string name, Vector4 vec)
        {
            if (!_uniformLocDictionary.TryGetValue(name, out var value))
            {
                value = GL.GetUniformLocation(_programId, name);
                _uniformLocDictionary[name] = value;
            }

            GL.Uniform4(value, vec);
        }

        public void SetUniformMatrix4(string name, Matrix4 matrix)
        {
            if (!_uniformLocDictionary.TryGetValue(name, out var value))
            {
                value = GL.GetUniformLocation(_programId, name);
                _uniformLocDictionary[name] = value;
            }

            GL.UniformMatrix4(value, true, ref matrix);
        }
    }
}
