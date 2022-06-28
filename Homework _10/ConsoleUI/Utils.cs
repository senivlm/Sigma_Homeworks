using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal static class Utils
    {
        public static List<string> GetText(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            List<string> result = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                    result.Add(reader.ReadLine());
            }

            return result;
        }
    }
}
