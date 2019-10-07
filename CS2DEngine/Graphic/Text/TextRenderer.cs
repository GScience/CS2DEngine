using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace CS2DEngine.Graphic.Text
{
    public class TextRenderer
    {
        private static readonly Bitmap ImageBuffer = new Bitmap(1024, 1024);

        public readonly FontFamily fontFamily;
        public readonly float fontSize;

        public readonly Font font;

        public TextRenderer(FontFamily fontFamily, float fontSize)
        {
            this.fontFamily = fontFamily;
            this.fontSize = fontSize;
            font = new Font(fontFamily, this.fontSize);
        }

        public CharGraph[] AutoCreateCharGraphs()
        {
            var charList = new List<char>();

            foreach (var value in Localization.GetValueArray())
            foreach (var c in value)
                if (!charList.Contains(c))
                    charList.Add(c);

            return CreateCharGraphs(charList.ToArray());
        }

        public CharGraph[] CreateCharGraphs(char[] chars)
        {
            var graphList = new List<CharGraph>(chars.Length);
            var graphics = Graphics.FromImage(ImageBuffer);

            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            var drawingOffset = Vector2.Zero;
            var lineMaxHeight = 0;

            foreach (var c in chars)
            {
                var charSize = graphics.MeasureString("" + c, font);

                var charWidth = (int) Math.Ceiling(charSize.Width);
                var charHeight = (int) Math.Ceiling(charSize.Height);

                //换行
                if (drawingOffset.X + charWidth > ImageBuffer.Width)
                {
                    drawingOffset.X = 0;
                    drawingOffset.Y += lineMaxHeight;
                    lineMaxHeight = 0;
                }

                if (drawingOffset.Y + charHeight > ImageBuffer.Height)
                {
                    BindTextureToNewCharGraph(graphList, GenTexture());
                    graphics.Clear(Color.FromArgb(0, 255, 255, 255));
                    drawingOffset = Vector2.Zero;
                }

                //创建字符图形
                graphList.Add(new CharGraph(
                    (int)Math.Ceiling(drawingOffset.X), 
                    (int)Math.Ceiling(drawingOffset.Y), 
                    charWidth, 
                    charHeight, 
                    null));

                graphics.DrawString("" + c, font, new SolidBrush(Color.White), drawingOffset.X, drawingOffset.Y);

                drawingOffset.X += charWidth;

                lineMaxHeight = Math.Max(lineMaxHeight, charHeight);
            }

            BindTextureToNewCharGraph(graphList, GenTexture());

            graphics.Dispose();

            return graphList.ToArray();
        }

        private Texture GenTexture()
        {
            var texture = Texture.Create(
                ImageBuffer,
                0,
                0,
                ImageBuffer.Width,
                ImageBuffer.Height);

            return texture;
        }

        private void BindTextureToNewCharGraph(List<CharGraph> list, Texture texture)
        {
            for (var i = list.Count - 1; i >= 0; --i)
                list[i].texture = texture;
        }
    }
}
