using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShopLib.Storage.Logger
{
    /// <summary>
    /// Logs and retrieves lines that can't be serialized into Storage object 
    /// </summary>
    public static class Logger
    {
        private static readonly string _logPath = Path.Combine(Environment.CurrentDirectory, "log.txt");//@"C:\Users\Иван\source\repos\Homework 7\log.txt";
        public static void Log(string message)
        {
            using (StreamWriter sr = new StreamWriter(_logPath, true))
            {
                sr.WriteLine($"{DateTime.Now} - {message}");
            }
        }

        public static List<string> GetLogs()
        {
            return _logPath.LoadFile();
        }

        public static List<string> After(this List<string> logs, DateTime dt)
        {
            List<string> result = new List<string>();

            foreach (var line in logs)
            {
                string[] data = line.Split('-');

                if (DateTime.Parse(data[0].Trim()).CompareTo(dt) > 0)
                    result.Add(line);
            }

            return result;
        }
    }
}
