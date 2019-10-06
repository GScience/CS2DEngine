using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine.Scene.Widget;

namespace CS2DEngine.Scene
{
    /// <summary>
    /// 场景
    /// </summary>
    public abstract class Scene : WidgetContainer
    {
        public SceneState SceneState { get; protected set; }

        /// <summary>
        /// 场景刷新
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// 当场景加载
        /// </summary>
        public abstract void OnLoad();

        /// <summary>
        /// 当场景卸载
        /// </summary>
        public abstract void OnUnloading();

        internal void Unload()
        {
            OnUnloading();
        }
    }
}
