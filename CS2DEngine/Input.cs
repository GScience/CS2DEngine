using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace CS2DEngine
{
    public static class Input
    {
        private static readonly bool[] IsMouseButtonPressedLastFrame = new bool[3];

        /// <summary>
        /// 获取键盘是否按住
        /// </summary>
        /// <param name="key">需要获取的按键</param>
        /// <returns>是否按住</returns>
        public static bool GetKey(Key key)
        {
            return Keyboard.GetState()[Key.W];
        }

        /// <summary>
        /// 获取键盘是否按下
        /// </summary>
        /// <param name="key">需要获取的按键</param>
        /// <returns>是否抬起</returns>
        public static bool GetKeyDown(Key key)
        {
            return Keyboard.GetState().IsKeyDown(Key.W);
        }

        /// <summary>
        /// 获取键盘是否松开
        /// </summary>
        /// <param name="key">需要获取的按键</param>
        /// <returns>是否松开</returns>
        public static bool GetKeyUp(Key key)
        {
            return Keyboard.GetState().IsKeyUp(Key.W);
        }

        /// <summary>
        /// 获取键盘是否按下任意键
        /// </summary>
        /// <returns>是否按下任意键</returns>
        public static bool IsAnyKeyDown()
        {
            return Keyboard.GetState().IsAnyKeyDown;
        }

        /// <summary>
        /// 获取鼠标按键是否按住
        /// </summary>
        /// <param name="button">0:左键 1:右键 2:中键</param>
        /// <returns>是否按下</returns>
        public static bool GetMouseButton(int button)
        {
            switch (button)
            {
                case 0:
                    return Mouse.GetState().LeftButton == ButtonState.Pressed;
                case 1:
                    return Mouse.GetState().RightButton == ButtonState.Pressed;
                case 2:
                    return Mouse.GetState().MiddleButton == ButtonState.Pressed;
                default:
                    throw new ArgumentException("button only accept 0, 1 or 2");
            }
        }

        /// <summary>
        /// 获取鼠标按键是否按下
        /// </summary>
        /// <param name="button">0:左键 1:右键 2:中键</param>
        /// <returns>是否按下</returns>
        public static bool GetMouseButtonDown(int button)
        {
            if (button < 3 && button >= 0 && IsMouseButtonPressedLastFrame[button])
                return false;

            switch (button)
            {
                case 0:
                    return Mouse.GetState().IsButtonDown(MouseButton.Left);
                case 1:
                    return Mouse.GetState().IsButtonDown(MouseButton.Right);
                case 2:
                    return Mouse.GetState().IsButtonDown(MouseButton.Middle);
                default:
                    throw new ArgumentException("button only accept 0, 1 or 2");
            }
        }

        /// <summary>
        /// 获取鼠标按键是否按下
        /// </summary>
        /// <param name="button">0:左键 1:右键 2:中键</param>
        /// <returns>是否按下</returns>
        public static bool GetMouseButtonUp(int button)
        {
            switch (button)
            {
                case 0:
                    return Mouse.GetState().IsButtonUp(MouseButton.Left);
                case 1:
                    return Mouse.GetState().IsButtonUp(MouseButton.Right);
                case 2:
                    return Mouse.GetState().IsButtonUp(MouseButton.Middle);
                default:
                    throw new ArgumentException("button only accept 0, 1 or 2");
            }
        }

        /// <summary>
        /// 刷新
        /// 引擎内部使用
        /// </summary>
        internal static void Update()
        {
            for (var i = 0; i < 3; ++i)
                IsMouseButtonPressedLastFrame[i] = GetMouseButton(i);
        }
    }
}
