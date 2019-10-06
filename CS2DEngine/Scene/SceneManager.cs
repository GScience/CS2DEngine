using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace CS2DEngine.Scene
{
    internal static class SceneManager
    {
        private static readonly Dictionary<Type, Scene> SceneDictionary = new Dictionary<Type, Scene>();

        private static Scene _currentScene;
        private static Scene _nextScene;

        internal static void Update()
        {
            if (_currentScene?.SceneState == SceneState.Unloaded || _currentScene == null)
            {
                _nextScene?.OnLoad();
                _currentScene = _nextScene;
            }

            _currentScene?.Update();
        }

        internal static void Draw()
        {
            _currentScene?.Draw();
        }

        public static T RegisterScene<T>() where T : Scene, new()
        {
            var scene = new T();
            SceneDictionary[typeof(T)] = scene;
            return scene;
        }

        public static void SwitchToScene(Type type)
        {
            if (type == null)
                return;

            _currentScene?.Unload();
            _nextScene = SceneDictionary[type];
        }

        public static void SwitchToScene<T>()
        {
            SwitchToScene(typeof(T));
        }

        public static Scene GetScene(Type t)
        {
            return SceneDictionary[t];
        }

        public static T GetScene<T>() where T : Scene
        {
            return (T)GetScene(typeof(T));
        }
    }
}
