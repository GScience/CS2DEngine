using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Graphic;
using CS2DEngine.Graphic.Text;

namespace CS2DEngine.Scene.Widget
{
    public class TextWidget : ImageWidget
    {
        private TextRenderer _textRenderer;
        private CharGraph[] charGraphArray;

        public float fontSize = 100;

        public TextWidget(Widget parent) : base(parent)
        {

        }

        public override void Refresh()
        {
            _textRenderer = new TextRenderer(UIFont, fontSize);
            charGraphArray = _textRenderer.AutoCreateCharGraphs();

            zOrder = 0.1f;

            base.Refresh();
            color = Color.Red;

            image = charGraphArray[0].texture;
        }
    }
}
