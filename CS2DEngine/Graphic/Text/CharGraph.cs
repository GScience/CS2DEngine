using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Graphic.Text
{
    public class CharGraph
    {
        public readonly int x;
        public readonly int y;
        public readonly int width;
        public readonly int height;

        public Texture texture;

        public CharGraph(int x, int y, int width, int height, Texture texture)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.texture = texture;
        }
    }
}
