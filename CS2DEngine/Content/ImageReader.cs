﻿using System;
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
        public object Load(Stream stream, string key)
        {
            var image = Image.FromStream(stream);
            var texture = Texture.Create(image);
            image.Dispose();

            return texture;
        }
    }
}
