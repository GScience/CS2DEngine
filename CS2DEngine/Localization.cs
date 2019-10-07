using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine
{
    public static class Localization
    {
        private static readonly Dictionary<string, string> Dictionary = new Dictionary<string, string>();

        public static string Name => System.Globalization.CultureInfo.CurrentCulture.Name;

        public static void Add(string key, string name, string value)
        {
            Dictionary[name.ToLower() + "." + key] = value;
        }

        public static string Get(string key, string name = "")
        {
            if (name == "")
                name = Name;

            var dictKey = Name.ToLower() + "." + key;

            return Dictionary.TryGetValue(dictKey, out var value) ? value : dictKey;
        }

        public static string[] GetValueArray()
        {
            return Dictionary.Values.ToArray();
        }
    }
}
