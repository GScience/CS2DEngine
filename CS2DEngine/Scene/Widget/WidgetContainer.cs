using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Scene.Widget
{
    public class WidgetContainer : Widget
    {
        public List<Widget> childList = new List<Widget>();

        public override void Draw()
        {
            foreach (var child in childList)
                child.Draw();
        }

        protected T AddWidget<T>() where T : Widget, new()
        {
            var widget = new T();
            widget.SetParent(this);
            childList.Add(widget);
            return widget;
        }
    }
}
