using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Content;
using CS2DEngine.Graphic;
using CS2DEngine.Graphic.Text;
using OpenTK;

namespace CS2DEngine.Scene.Widget
{
    public struct Margin
    {
        public int top, left, right, button;

        public Margin(int top, int left): this(top, left, int.MaxValue,int.MaxValue)
        {
        }

        public Margin(int top, int left, int right, int button)
        {
            this.top = top;
            this.left = left;
            this.right = right;
            this.button = button;
        }
    }

    public abstract class Widget
    {
        protected Widget(Widget parent)
        {
            this.parent = parent;
        }

        public readonly Widget parent;
        public Margin Margin { get; protected set; }

        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }

        public abstract void Draw();
        public abstract void Refresh();

        protected static T Create<T>(Widget parent) where T : Widget
        {
            var type = typeof(T);
            var cons = type.GetConstructor(new[] {typeof(Widget)});
            return (T) cons?.Invoke(new object[] {parent});
        }
    }
}
