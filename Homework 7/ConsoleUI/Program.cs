using ProductLibrary;
using ProductLibrary.Storage;
using System;
using static ProductLibrary.Utils;

namespace ConsoleUI
{
    // Represents UI layer
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = null;

            filePath = InputFilePath();

            filePath = @"C:\Users\Иван\source\repos\Homework 7\product_test1.txt";

            Storage storage;
            bool success;
            (storage, success) = filePath.LoadFile().ConvertToStorage();

            if (success)
                Console.WriteLine("All lines were successfully parsed.");
            else
                Console.WriteLine("Not all lines were parsed. Check log file.");

            Console.WriteLine();

            PrintLogs();

            Console.WriteLine();

            AddProducts(storage);
        }

        private static string InputFilePath()
        {
            string filePath = "";

            int tries = 3;
            do
            {
                Console.WriteLine("Enter full file path. Try pasting with right click.");
                filePath = Console.ReadLine();
                Console.Clear();

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Invalid file path");
                    tries--;
                    if (tries == 0)
                    {
                        //throw new Exception("Good bye");
                        Console.WriteLine("Good bye");
                        Environment.Exit(0);
                    }
                    Console.WriteLine($"Tries left: {tries}");
                }
                else
                {
                    break;
                }
            } while (!File.Exists(filePath) && tries > 0);

            return filePath;
        }

        private static void PrintLogs()
        {
            DateTime dt;
            do
            {
                Console.Write("Print logs after (date) : ");
            } while (!DateTime.TryParse(Console.ReadLine(), out dt));

            var logs = Logger.GetLogs().After(dt);

            int i = 1;
            Console.WriteLine("Lines that were not parsed:");
            foreach (var log in logs)
            {
                Console.WriteLine($"{i++}. {log}");
            }
        }

        private static void AddProducts(Storage storage)
        {
            Console.WriteLine("Do you want to add new products into this storage? (y/n)");

            if(Console.ReadLine().ToLower() == "y")
            {
                ProductLibrary.Storage.ConsoleInput.AddProducts(storage);
            }
        }
    }
}