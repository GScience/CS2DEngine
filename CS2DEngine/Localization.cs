using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine
{
    using StrDictionary = Dictionary<string, string>;

    public static class Localization
    {
        private static readonly Dictionary<string, StrDictionary> Dictionary = new Dictionary<string, StrDictionary>();

        public static string LangName => System.Globalization.CultureInfo.CurrentCulture.Name;

        public static void Add(string key, string langName, string value)
        {
            var langKey = langName.ToLower();

            if (!Dictionary.ContainsKey(langKey))
                Dictionary[langKey] = new StrDictionary();

            Dictionary[langKey][key] = value;
        }

        public static string Get(string key, string langName = "")
        {
            if (langName == "")
                langName = LangName;

            langName = langName.ToLower();

            if (!Dictionary.TryGetValue(langName, out var strDict))
                strDict = Dictionary.Values.First();

            return strDict.TryGetValue(key, out var value) ? value : key;
        }

        public static string[] GetValueArray()
        {
            var strList = Dictionary.SelectMany(strDict => strDict.Value.Values).ToList();

            return strList.ToArray();
        }
    }
}
