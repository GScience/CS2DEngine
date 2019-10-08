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
        public float fontSize = 100;
        public TextRenderer textRenderer;

        public TextWidget(Widget parent) : base(parent)
        {

        }

        public override void Refresh()
        {
            textRenderer.RefreshCharGraphs();

            var charGraph = textRenderer.GetCharGraph('a');

            ZOrder = 0.1f;
            Color = Color.Red;

            Image = charGraph.texture;
        }
    }
}
