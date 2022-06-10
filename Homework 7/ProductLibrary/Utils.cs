using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductLibrary
{
    public static class Utils
    {
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }
    }
}
