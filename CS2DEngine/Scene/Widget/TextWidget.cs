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
    public class TextWidget : Widget
    {
        public float fontSize = 100;
        public TextRenderer textRenderer;

        public TextWidget(Widget parent) : base(parent)
        {

        }

        public override void Draw()
        {
            textRenderer.DrawText("EngineInternal.InitEngine", 0, 0, 1, 1, Color.Red);
        }

        public override void Refresh()
        {
            textRenderer.RefreshCharGraphs();
        }
    }
}
