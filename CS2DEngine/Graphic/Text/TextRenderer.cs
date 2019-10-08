using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Content;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Graphic.Text
{
    public class TextRenderer
    {
        private static readonly VertexArrayObj Vao = VertexArrayObj.Create();
        private static readonly VertexBuffer Buffer = VertexBuffer.Create(BufferTarget.ArrayBuffer);

        private static readonly ShaderProgram Shader
            = ContentManager.Load<ShaderProgram, ShaderProgramReader>("[CS2DEngine]CS2DEngine.Shader.Text.shader");

        private static readonly float[] BufferData =
        {
            -1.0f, -1.0f, 0.0f, 1.0f,
            1.0f, -1.0f, 1.0f, 1.0f,
            -1.0f, 1.0f, 0.0f, 0.0f,
            1.0f, -1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 0.0f,
            -1.0f, 1.0f, 0.0f, 0.0f
        };

        private static readonly Bitmap ImageBuffer;

        private readonly Dictionary<char, CharGraph> _charGraphDictionary = new Dictionary<char, CharGraph>();

        public readonly FontFamily fontFamily;
        public readonly float fontSize;

        public readonly Font font;

        static TextRenderer()
        {
            var windowSize = Engine.WindowSize;
            ImageBuffer = new Bitmap(windowSize.Width, windowSize.Height);

            Buffer.BufferData(BufferData);
            Vao.SetAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            Vao.SetAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            Vao.SetVertexAttribArrayEnable(0);
            Vao.SetVertexAttribArrayEnable(1);
        }

        public TextRenderer(FontFamily fontFamily, float fontSize)
        {
            this.fontFamily = fontFamily;
            this.fontSize = fontSize;
            font = new Font(fontFamily, this.fontSize);
        }

        public void RefreshCharGraphs()
        {
            var charList = new List<char>();

            foreach (var value in Localization.GetValueArray())
            foreach (var c in value)
                if (!charList.Contains(c) && !_charGraphDictionary.ContainsKey(c))
                    charList.Add(c);

            var charGraphs = CreateCharGraphs(charList.ToArray());

            for (var i = 0; i < charGraphs.Length; ++i)
                _charGraphDictionary[charList[i]] = charGraphs[i];
        }

        public CharGraph GetCharGraph(char c)
        {
            return _charGraphDictionary.TryGetValue(c, out var value) ? value : null;
        }

        private CharGraph[] CreateCharGraphs(char[] chars)
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
                    BindTextureToNewCharGraph(graphList, GenTextureFromBuffer());
                    graphics.Clear(Color.FromArgb(0, 255, 255, 255));
                    drawingOffset = Vector2.Zero;
                }

                //创建字符图形
                var windowSize = Engine.WindowSize;

                graphList.Add(new CharGraph(
                    drawingOffset.X / windowSize.Width, 
                    drawingOffset.Y / windowSize.Height, 
                    (float) charWidth / windowSize.Width,
                    (float)charHeight / windowSize.Height, 
                    null));

                graphics.DrawString("" + c, font, new SolidBrush(Color.White), drawingOffset.X, drawingOffset.Y);

                drawingOffset.X += charWidth;

                lineMaxHeight = Math.Max(lineMaxHeight, charHeight);
            }

            BindTextureToNewCharGraph(graphList, GenTextureFromBuffer());

            graphics.Dispose();

            return graphList.ToArray();
        }

        private Texture GenTextureFromBuffer()
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
            {
                if (list[i].texture != null)
                    break;
                list[i].texture = texture;
            }
        }

        private Matrix4 GetTransformMatrix(float zOrder, float x, float y, float scaleX, float scaleY)
        {
            var matrix = Matrix4.Identity;
            matrix *= Matrix4.CreateTranslation(0, 0, zOrder);
            matrix *= Matrix4.CreateScale(scaleX, scaleY, 1);
            matrix *= Matrix4.CreateTranslation(x, y, 0);

            return matrix;
        }

        public void DrawText(LocalizedString text, float x, float y, float width, float height, Color color, float zOrder = 0.0f)
        {
            string str = text;
            var c = str[1];
            var charGraph = _charGraphDictionary[c];

            charGraph.texture.Bind();
            Vao.Bind();
            Buffer.Bind();
            Shader.Use();
            Shader.SetUniform4("color", new Vector4(color.R, color.G, color.B, color.A) / 255.0f);
            Shader.SetUniformMatrix4("transform", GetTransformMatrix(zOrder, charGraph.x, charGraph.y, charGraph.width, charGraph.height));

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }
    }
}
