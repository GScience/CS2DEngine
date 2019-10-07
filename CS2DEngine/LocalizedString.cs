using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine
{
    public class LocalizedString
    {
        private readonly string _str;

        public LocalizedString(string str)
        {
            _str = str;
        }

        public static implicit operator LocalizedString(string str)
        {
            return new LocalizedString(str);
        }

        public static implicit operator string(LocalizedString str)
        {
            return Localization.Get(str._str);
        }
    }
}
