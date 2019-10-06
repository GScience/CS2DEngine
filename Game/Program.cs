using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS2DEngine;

namespace Game
{
    public static class Program
    {
        public static void Launch()
        {
            Engine.Create(1280, 960, false, "MyGame").Run();
        }
    }
}
