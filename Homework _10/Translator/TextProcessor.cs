using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TranslatorLib
{
    internal static class TextProcessor
    {
        public static Dictionary<string, string> ConvertToDictionary(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            Dictionary<string, string> result = new Dictionary<string, string>();
            int lineCount = 1;

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string temp = reader.ReadLine();

                    var str = temp.Split('-');

                    if (str.Length != 2)
                        throw new InvalidDataException("Could not parse line " + lineCount);

                    result.Add(str[0], str[1]);
                    lineCount++;
                }
            }
            return result;
        }
    }
}
