using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Graphic.Text
{
    public class CharGraph
    {
        public readonly float x;
        public readonly float y;
        public readonly float width;
        public readonly float height;

        public Texture texture;

        public CharGraph(float x, float y, float width, float height, Texture texture)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.texture = texture;
        }
    }
}
