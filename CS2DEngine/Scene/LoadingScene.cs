using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Content;
using CS2DEngine.Graphic;
using CS2DEngine.Scene.Widget;
using OpenTK.Graphics.ES20;
using OpenTK.Input;

namespace CS2DEngine.Scene
{
    internal class LoadingScene : Scene
    {
        /// <summary>
        /// 场景状态
        /// </summary>
        internal struct InitState
        {
            public InitState(Action func, LocalizedString state)
            {
                this.func = func;
                this.state = state;
            }

            public Action func;
            public LocalizedString state;
        }

        /// <summary>
        /// 下一个场景
        /// </summary>
        private Type _nextScene;

        private readonly List<InitState> _loadingStateList = new List<InitState>();

        private int _loadingState;

        /// <summary>
        /// 设置第一个场景
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetFirstScene<T>()
        {
            _nextScene = typeof(T);
        }

        /// <summary>
        /// 添加加载阶段
        /// </summary>
        /// <param name="action"></param>
        public void AddLoadingState(InitState action)
        {
            _loadingStateList.Add(action);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public override void Update()
        {
            if (_loadingState >= _loadingStateList.Count)
            {
                if (_nextScene != null)
                    SceneManager.SwitchToScene(_nextScene);
                return;
            }

            var initState = _loadingStateList[_loadingState++];
            initState.func();
            Logger.Info(initState.state);
        }

        /// <summary>
        /// 当场景加载
        /// </summary>
        public override void OnLoad()
        {
            var image = AddWidget<ImageWidget>();
            var text = AddWidget<TextWidget>();
            image.image = ContentManager.Load<Texture, ImageReader>("[CS2DEngine]CS2DEngine.Image.Loading.png");
            ContentManager.Load<object, LangFileReader>("[CS2DEngine]CS2DEngine.Lang.EngineInternal.lang");
        }

        /// <summary>
        /// 当场景卸载
        /// </summary>
        public override void OnUnloading()
        {
            
        }
    }
}
