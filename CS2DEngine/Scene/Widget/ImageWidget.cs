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
        public Texture image;

        public override void Draw()
        {
            image.Bind();

            GL.Begin(PrimitiveType.Quads);

            GL.TexCoord2(0.0f, 1.0f);
            //GL.Color3(1.0f,0,0);
            GL.Vertex3(-1.0f, -1.0f, 0.0f);

            GL.TexCoord2(0.0f, 0.0f);
            //GL.Color3(0, 1.0f, 0);
            GL.Vertex3(-1.0f, 1.0f, 0.0f);

            GL.TexCoord2(1.0f, 0.0f);
            //GL.Color3(0, 0, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 0.0f);

            GL.TexCoord2(1.0f, 1.0f);
            //GL.Color3(0, 0, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 0.0f);

            GL.End();
        }
    }
}
