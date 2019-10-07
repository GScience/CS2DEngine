using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Graphic
{
    public class Texture
    {
        private readonly int _textureId;

        private Texture(int textureId)
        {
            _textureId = textureId;
        }

        /// <summary>
        /// 绑定纹理
        /// </summary>
        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, _textureId);
        }

        public static Texture Create(Image image, int x = 0, int y = 0, int width = 0, int height = 0)
        {
            //创建纹理
            var textureId = GL.GenTexture();

            if (width == 0)
                width = image.Width;
            if (height == 0)
                height = image.Height;

            var bitmap = ((Bitmap) image).Clone(new Rectangle(x,y,width, height), image.PixelFormat);
            var rect = new Rectangle(0, 0, width, height);

            var bitmapData = bitmap.LockBits(
                rect, 
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                bitmap.PixelFormat);

            PixelInternalFormat internalFormat;
            PixelFormat pixelFormat;

            switch (bitmapData.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    internalFormat = PixelInternalFormat.Rgb;
                    pixelFormat = PixelFormat.Bgr;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    internalFormat = PixelInternalFormat.Rgba;
                    pixelFormat = PixelFormat.Bgra;
                    break;
                default:
                    throw new ArgumentException("Unknown format " + bitmapData.PixelFormat);
            }
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.TexImage2D(TextureTarget.Texture2D,
                0,
                internalFormat,
                bitmapData.Width,
                bitmapData.Height,
                0,
                pixelFormat,
                PixelType.UnsignedByte,
                bitmapData.Scan0);

            //设置纹理属性
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest); // 线形滤波
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Nearest); // 线形滤波

            bitmap.Dispose();

            return new Texture(textureId);
        }

        ~Texture()
        {
            Dispose();
        }

        public static void Unbind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public void Dispose()
        {
            if (!Engine.IsExiting)
                GL.DeleteTexture(_textureId);

            GC.SuppressFinalize(this);
        }
    }
}
