using EnergyAccounting;
using System;
using System.Globalization;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // завдання 1
            var r = new Report(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\test1.txt");

            r.PrintToFile(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\table.txt");

            // При відомій вартості кВт енергії знайти прізвище власника з найбільшою заборгованістю.
            string biggestDebtor = r.GetBiggestBill().LastName;

            // Знайти номер квартири, в якій не використовувалась електроенергія.
            string numflat = r.FlatReports.Where(x => x.ConsumedSum == 0).FirstOrDefault()?.FlatNumber;

            //Видрукувати інформацію про те, скільки днів пройшло з моменту останнього зняття показу лічильника до поточної дати.
            DateTime date = DateTime.Now;
            TimeSpan timePast = date - r.GetLastDate();
            Console.WriteLine(timePast.Days + " days");

            // завдання 2
            FileClass fc = new FileClass();
            fc.ReadFile(new StreamReader(File.OpenRead(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\task2.txt")));
        }
    }
}