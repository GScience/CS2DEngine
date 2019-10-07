using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Graphic;

namespace CS2DEngine.Scene.Widget
{
    public class TextWidget : ImageWidget
    {
        private TextRenderer _textRenderer;
        public float fontSize = 50;

        public TextWidget(Widget parent) : base(parent)
        {
            _textRenderer = new TextRenderer(UIFont, fontSize);
        }

        public override void Refresh()
        {
            base.Refresh();
            for (var i = 0; i < 256; ++i)
            {
                image?.Dispose();
                image = _textRenderer.Draw(50, 50, "" + (char) i, Color.Crimson);
            }

            image = _textRenderer.Draw(50, 50, "A", Color.Crimson);
        }
    }
}
