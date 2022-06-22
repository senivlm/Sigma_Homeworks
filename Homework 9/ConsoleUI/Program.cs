using MenuLib;
using MenuLib.Entity;
using MenuLib.Exceptions;
using MenuLib.Service;
using System;
using System.Collections;
using System.Globalization;

namespace ConsoleUI
{
    internal class Program
    {
        private readonly static string _menuFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 9\menu.txt";
        private readonly static string _pricesFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 9\prices.txt";

        static void Main(string[] args)
        {
            Menu menu = null;
            IngredientPrices ip = null;
            Currency currency = 0;
            decimal exchangeRate = 0;

            try
            {
                // init from files
                menu = new Menu(_menuFile);
                ip = new IngredientPrices(_pricesFile);

                // choose currency
                (currency, exchangeRate) = CurrencyInput.ChooseCurrency();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }



            int tries = 2;
            while (tries >= 0)
            {
                try
                {
                    decimal totalPriceGRN = menu.GetFullPrice(ip);
                    decimal totalPriceInCurrency = totalPriceGRN / exchangeRate;
                    string priceMessage = GetPriceMessage(currency, totalPriceInCurrency);
                    WritePriceIntoFile(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 9\result.txt", priceMessage);
                    Console.WriteLine(priceMessage);
                    break;
                }
                catch (NoIngredientPriceException ex)
                {
                    FailedTryMessage(tries, ex);

                    // input price from console
                    bool success = AddIngredientPrice(ip);
                    if (!success)
                        tries--;
                    
                    if (tries == 0)
                    {
                        ShutdownMessage();
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    break;
                }
            }
        }
        private static void WritePriceIntoFile(string filePath, string price)
        {
            File.WriteAllText(filePath, price);
        }

        private static string GetPriceMessage(Currency currency, decimal totalPriceInCurrency)
        {
            string priceMessage = "";
            switch (currency)
            {
                case Currency.USD:
                    priceMessage = $"Total price: {totalPriceInCurrency.ToString("C2", new CultureInfo("en-US"))}";
                    break;
                case Currency.EUR:
                    priceMessage = $"Total price: {totalPriceInCurrency.ToString("C2", new CultureInfo("fr-FR"))}";
                    break;
            }
            return priceMessage;
        }

        private static void FailedTryMessage(int tries, NoIngredientPriceException ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.WriteLine($"You have {tries} tries left.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void ShutdownMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You ran out of tries");
            Console.WriteLine("Good bye");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static bool AddIngredientPrice(IngredientPrices ip)
        {
            Console.Write("Enter ingredient name: ");
            Ingredient ingredient = new Ingredient(Console.ReadLine());

            Console.Write("Enter ingredient's price: ");
            decimal price;
            bool priceIsValid = decimal.TryParse(Console.ReadLine(), out price);

            if(priceIsValid == false)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Price is not valid.");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
                
            var pair = new KeyValuePair<Ingredient, decimal>(ingredient, price);
            ip.Add(pair);

            return true;
        }
    }
}