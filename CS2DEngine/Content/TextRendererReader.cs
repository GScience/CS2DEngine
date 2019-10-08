using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Graphic.Text;

namespace CS2DEngine.Content
{
    internal class TextRendererReader : IContentReader
    {
        public object Load(Stream stream, string key)
        {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            var privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddMemoryFont(Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), buffer.Length);

            var fontFamily = privateFontCollection.Families[0];

            var keys = key.Split('.');
            var fontSize = int.Parse(keys[keys.Length - 2]) * Engine.WindowSize.Height / 100;

            return new TextRenderer(fontFamily, fontSize);
        }
    }
}
