using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Widget Parent { get; protected set; }
        public Margin Margin { get; protected set; }

        public int MaxHeight { get; set; }
        public int MaxWidth { get; set; }

        internal void SetParent(Widget widget)
        {
            Parent = widget;
        }

        public abstract void Draw();
    }
}
