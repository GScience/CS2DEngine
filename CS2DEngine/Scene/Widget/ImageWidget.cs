using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Content;
using CS2DEngine.Graphic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine.Scene.Widget
{
    public class ImageWidget : Widget
    {
        private readonly UISpriteRenderer _spriteRenderer = new UISpriteRenderer();

        public float ZOrder
        {
            get => _spriteRenderer.zOrder;
            set => _spriteRenderer.zOrder = value;
        }

        public Texture Image
        {
            get => _spriteRenderer.texture;
            set => _spriteRenderer.texture = value;
        }

        public Color Color
        {
            get => _spriteRenderer.color;
            set => _spriteRenderer.color = value;
        }

        public ImageWidget(Widget parent) : base(parent)
        {
            
        }

        public override void Refresh()
        {
            ZOrder = 0.2f;
        }

        public override void Draw()
        {
            _spriteRenderer.Draw();
        }
    }
}
