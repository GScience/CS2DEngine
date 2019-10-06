using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS2DEngine
{
    public static class Logger
    {
        public static void Info(string log)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}][Info] {log}");
        }

        public static void Warning(string log)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}][Warning] {log}");
        }

        public static void Error(string log)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}][ERROR] {log}");
        }

        public static void Crash(string log)
        {
            Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}][C R A S H] {log}");
        }
    }
}
