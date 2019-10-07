using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Graphic;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Content
{
    internal class ShaderProgramReader : IContentReader
    {
        public object Load(Stream stream)
        {
            var reader = new StreamReader(stream);
            var shaderProgram = ShaderProgram.Create();
            var macro = "";
            var shaderTypeBuffer = "";
            int buffer;

            while ((buffer = reader.Read()) > 0)
            {
                if (buffer == '#')
                    macro += "#" + reader.ReadLine();
                else if (buffer == '{')
                {
                    var shaderSource = macro + "\n" + ReadShaderSource(reader);
                    Enum.TryParse(shaderTypeBuffer, out ShaderType shaderType);
                    LoadShader(shaderType, shaderSource, shaderProgram.GetProgramId());
                    shaderTypeBuffer = "";
                }
                else if (!char.IsLetter((char) buffer))
                    continue;
                else
                    shaderTypeBuffer += (char) buffer;
            }

            GL.LinkProgram(shaderProgram.GetProgramId());
            GL.GetProgram(shaderProgram.GetProgramId(), GetProgramParameterName.LinkStatus, out var state);
            if (state != 1)
                Logger.Info(GL.GetProgramInfoLog(shaderProgram.GetProgramId()));
            return shaderProgram;
        }

        private void LoadShader(ShaderType type, string shaderSource, int program)
        {
            var shaderId = GL.CreateShader(type);
            GL.ShaderSource(shaderId, shaderSource);
            GL.CompileShader(shaderId);
            GL.AttachShader(program, shaderId);
            GL.GetShader(shaderId, ShaderParameter.CompileStatus, out var state);
            if (state != 1)
                Logger.Info(GL.GetShaderInfoLog(shaderId));
        }

        public string ReadShaderSource(StreamReader reader)
        {
            int buffer;
            var shaderSource = "";
            var depth = 1;
            
            while ((buffer = reader.Read()) > 0)
            {
                if (buffer == '{')
                    depth++;
                else if (buffer == '}')
                    depth--;

                if (depth == 0)
                    return shaderSource;

                shaderSource += (char) buffer;
            }
            throw new InvalidDataException("Failed to read to the end of the shader");
        }
    }
}
