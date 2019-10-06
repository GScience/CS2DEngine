using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine.Content
{
    /// <summary>
    /// 资源接口
    /// </summary>
    public interface IContentReader
    {
        object Load(Stream stream);
    }
}
