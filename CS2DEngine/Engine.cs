using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Content;
using CS2DEngine.Scene;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace CS2DEngine
{
    /// <summary>
    /// 引擎
    /// </summary>
    public class Engine
    {
        private GameWindow _window;

        private Engine()
        {
        }

        /// <summary>
        /// 创建引擎
        /// </summary>
        /// <param name="windowName">窗体名称</param>
        /// <param name="width">窗体宽度</param>
        /// <param name="height">窗体高度</param>
        /// <param name="isFullScene">是否全屏</param>
        /// <returns>创建完成的引擎</returns>
        public static Engine Create(int width, int height, bool isFullScene, string windowName)
        {
            var engine = new Engine()
            {
                _window = new GameWindow(
                    width,
                    height, 
                    GraphicsMode.Default, 
                    windowName,
                    isFullScene ? GameWindowFlags.Fullscreen : GameWindowFlags.FixedWindow,
                    DisplayDevice.Default, 
                    2, 
                    0, 
                    GraphicsContextFlags.Default, 
                    null, 
                    true)
            };

            GL.Enable(EnableCap.Texture2D);
            GL.ClearColor(Color.DeepSkyBlue);

            SceneManager.RegisterScene<LoadingScene>();

            engine.RegisterInitState(engine.Init, "Init Engine");
            engine.RegisterInitState(ContentManager.Init, "Loading content");

            engine._window.UpdateFrame += engine.WindowOnUpdateFrame;
            
            return engine;
        }

        /// <summary>
        /// 每帧刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            Time.Update(e.Time);
            SceneManager.Update();
            Input.Update();
        }

        /// <summary>
        /// 渲染帧刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowOnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            SceneManager.Draw();

            _window.SwapBuffers();
        }

        /// <summary>
        /// 注册初始化阶段
        /// </summary>
        /// <param name="func"></param>
        /// <param name="state"></param>
        public void RegisterInitState(Action func, string state)
        {
            SceneManager.GetScene<LoadingScene>().AddLoadingState(new LoadingScene.InitState(func, state));
        }

        /// <summary>
        /// 设置第一个场景
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetFirstScene<T>()
        {
            SceneManager.GetScene<LoadingScene>().SetFirstScene<T>();
        }

        public void Init()
        {
            _window.RenderFrame += WindowOnRenderFrame;
        }

        public void Run()
        {
            SceneManager.SwitchToScene<LoadingScene>();
            _window.Run();
        }
    }
}
