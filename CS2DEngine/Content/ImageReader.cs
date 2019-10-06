using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Graphic;

namespace CS2DEngine.Content
{
    internal class ImageReader : IContentReader
    {
        public object Load(Stream stream)
        {
            var image = Image.FromStream(stream);
            return Texture.Create(image);
        }
    }
}
