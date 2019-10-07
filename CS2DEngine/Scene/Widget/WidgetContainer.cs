using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Scene.Widget
{
    public class WidgetContainer : Widget
    {
        public List<Widget> children = new List<Widget>();

        public override void Draw()
        {
            foreach (var child in children)
                child.Draw();
        }

        protected T AddWidget<T>() where T : Widget
        {
            var widget = Create<T>(this);
            children.Add(widget);
            return widget;
        }

        public override void Refresh()
        {
            foreach (var child in children)
                child.Refresh();
        }

        public WidgetContainer(Widget parent) : base(parent)
        {
        }
    }
}
