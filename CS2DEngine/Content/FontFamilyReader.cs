using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Content
{
    internal class FontFamilyReader : IContentReader
    {
        public object Load(Stream stream)
        {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            var privateFontCollection = new PrivateFontCollection();
            privateFontCollection.AddMemoryFont(Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), buffer.Length);

            return privateFontCollection.Families[0];
        }
    }
}
