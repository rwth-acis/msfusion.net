using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Core.Utilities
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        internal static void Log(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
