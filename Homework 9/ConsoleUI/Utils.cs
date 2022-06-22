using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal static class Utils
    {
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException();

            return File.ReadAllLines(file).ToList();
        }
    }
}
