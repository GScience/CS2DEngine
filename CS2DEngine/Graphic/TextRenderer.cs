using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Graphic
{
    public class TextRenderer
    {
        private static readonly Bitmap _imageBuffer = new Bitmap(500, 200);

        public readonly FontFamily fontFamily;
        public readonly float fontSize;

        public readonly Font font;

        public TextRenderer(FontFamily fontFamily, float fontSize)
        {
            this.fontFamily = fontFamily;
            this.fontSize = fontSize;
            font = new Font(fontFamily, this.fontSize);
        }

        public Texture Draw(int width, int height, string str, Color color)
        {       
            var graphics = Graphics.FromImage(_imageBuffer);
            graphics.Clear(Color.FromArgb(0, 255, 255, 255));
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            var charSize = graphics.MeasureString(str, font);

            graphics.Clip = new Region(new Rectangle(
                0,
                0,
                (int)Math.Ceiling(charSize.Width),
                (int)Math.Ceiling(charSize.Height)));

            graphics.DrawString(str, font, new SolidBrush(color), 0, 0);

            var texture = Texture.Create(
                _imageBuffer, 
                0, 
                0, 
                (int) Math.Ceiling(charSize.Width),
                (int) Math.Ceiling(charSize.Height));

            graphics.Dispose();

            return texture;
        }
    }
}
