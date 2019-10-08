using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Content
{
    internal class LangFileReader : IContentReader
    {
        public object Load(Stream stream, string key)
        {
            var reader = new StreamReader(stream);
            var keys = key.Split('.');
            ReadLang(reader, keys[keys.Length - 3] + "-" +keys[keys.Length - 2]);
            return null;
        }

        private void ReadLang(StreamReader reader, string name)
        {
            int buffer;
            var keyBuffer = "";

            while ((buffer = reader.Read()) != -1)
            {
                if (buffer == '=')
                {
                    var value = reader.ReadLine()?.TrimStart();
                    Localization.Add(keyBuffer, name, value);
                    keyBuffer = "";
                }
                else if (char.IsControl((char) buffer) || buffer == ' ')
                    continue;
                else
                    keyBuffer += (char) buffer;
            }
        }
    }
}
