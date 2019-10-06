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
        private int _textureId;

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

        public static Texture Create(Image image)
        {
            //创建纹理
            GL.GenTextures(1, out int textureId);
            var err = GL.GetError();

            var bitmap = (Bitmap) image;
            var rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

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
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear); // 线形滤波
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Linear); // 线形滤波
            return new Texture(textureId);
        }
    }
}
