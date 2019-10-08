using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Content
{
    /// <summary>
    /// 资源管理器
    /// 自动加载所有资源
    /// </summary>
    public static class ContentManager
    {
        public const string ContentPath = @"\Content\";

        private static readonly Dictionary<string, object> ContentDictionary 
            = new Dictionary<string, object>();

        private static readonly Dictionary<Type, IContentReader> ReaderDictionary 
            = new Dictionary<Type, IContentReader>();

        static ContentManager()
        {
            var assembly = typeof(ContentManager).Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(IContentReader).IsAssignableFrom(type) || type.IsAbstract)
                    continue;

                ReaderDictionary[type] = (IContentReader) Activator.CreateInstance(type);
            }
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <typeparam name="T">获取资源类型</typeparam>
        /// <param name="key">获取资源名</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return (T) ContentDictionary[key];
        }

        /// <summary>
        /// 手动添加资源
        /// </summary>
        /// <param name="key">资源名称</param>
        /// <param name="stream">资源流</param>
        /// <param name="readerType">资源读取器</param>
        public static T Load<T, T2>(string key)
        {
            var reader = ReaderDictionary[typeof(T2)];
            var obj = reader.Load(GetStreamFromKey(key), key);

            ContentDictionary[key] = obj;

            return (T)obj;
        }

        private static Stream GetStreamFromKey(string key)
        {
            if (key.First() == '[')
            {
                var assemblyName = key.Substring(1, key.LastIndexOf(']') - 1);
                var resName = key.Substring(key.LastIndexOf(']') + 1);

                var resAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(assembly => assembly.GetName().Name == assemblyName);

                if (resAssembly == null)
                {
                    Logger.Error("Failed to find assembly " + assemblyName);
                    return null;
                }

                return resAssembly.GetManifestResourceStream(resName);
            }

            return File.OpenRead(Environment.CurrentDirectory + ContentPath + key.Replace('.', '\\'));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        internal static void Init()
        {

        }
    }
}
