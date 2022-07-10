using ShopLib;
using ShopLib.Products;
using ShopLib.Storage;
using System;
using ShopLib.Storage.Input;
using RPNLib;
using System.Globalization;

namespace ConsoleUI
{
    internal class Program
    {
        private static string expiredPath = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework _12\expired.txt";
        static void Main(string[] args)
        {
            Storage<IProduct> storage = new Storage<IProduct>();
            storage.OnAddingExpired += Storage_OnAddingExpired;
            storage.ConvertToStorage(new List<string>());
            storage.Add(new Diary("milk", 10, 10, DateTime.Now.AddDays(-2)), 0);
            storage.Add(new Meat(), 30);

            // 2.   Здійсніть пошук товарів за різними ознаками. Запропонуйте варіант пошуку, який є максимально ефективним та універсальним.

            // Можна використовувати інтерфейси як ознаки

            List<IExpirable> expirables = new();

            foreach (var item in storage)
            {
                if (item.Key is IExpirable)
                {
                    expirables.Add((IExpirable)item.Key);
                }
            }

            string postfix;
            RPNLib.Config.DecimalSeparator = "."; //CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            //// "4 + 7 + 12 / 3"
            //// "4 7 + 12 3 / +"
            postfix = Infix.GetPostfixString("4.5+7 +12  /3");
            Console.WriteLine(postfix);
            Console.WriteLine(Postfix.Evaluate(postfix));

            RPNLib.Config.DecimalSeparator = ",";
            // 1 + cos ( 1 / 2 ) + sin ( 1 / 2 )
            postfix = Infix.GetPostfixString($"cos({Math.PI}) + sin         (        {Math.PI})");
            Console.WriteLine(postfix);
            Console.WriteLine(Postfix.Evaluate(postfix));
        }

        private static void Storage_OnAddingExpired(object? sender, OnAddingExpiredEventArgs e)
        {
            Storage<IProduct> storage = sender as Storage<IProduct>;

            if(storage != null)
            {
                if (e.ProductStock.Key != null)
                {
                    storage.Remove(e.ProductStock.Key);
                    LogExpired(e.ProductStock.Key); 
                }
            }
        }

        private static void LogExpired(IExpirable product)
        {
            using (StreamWriter writer = File.AppendText(expiredPath))
            {
                writer.Write($"{product.Name}-{product.ExpirationDate}");
                writer.Write("\n");
            }
        }
    }
}