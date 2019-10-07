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
        public object Load(Stream stream)
        {
            var reader = new StreamReader(stream);
            int buffer;
            var nameBuffer = "";

            while ((buffer = reader.Read()) != -1)
            {
                if (buffer == '[')
                    continue;

                if (buffer == ']')
                {
                    ReadLang(reader, nameBuffer);
                    nameBuffer = "";
                }
                else if (char.IsControl((char) buffer) || buffer == ' ')
                    continue;
                else
                    nameBuffer += (char)buffer;
            }

            return null;
        }

        private void ReadLang(StreamReader reader, string name)
        {
            int buffer;
            var keyBuffer = "";

            while ((buffer = reader.Read()) != -1)
            {
                if (buffer == '[')
                    break;
                
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
