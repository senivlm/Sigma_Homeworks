using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal static class CurrencyInput
    {
        private readonly static string _currencyPath = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 9\exchange_rate.txt";
        public static (Currency, decimal) ChooseCurrency()
        {
            Console.WriteLine("Choose currency:");
            Console.WriteLine("1. USD");
            Console.WriteLine("2. EURO");

            string choice = Console.ReadLine();

            if(choice != "1" && choice != "2")
                throw new InvalidDataException();

            Currency currency;
            decimal exchangeRate = 0;

            if (Enum.TryParse(choice, out currency))
            {
                Console.Clear();
                try
                {
                    exchangeRate = currency.GetExchangeRate();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return (currency, exchangeRate);
        }

        private static decimal GetExchangeRate(this Currency currency)
        {
            List<string> exchangeRates = _currencyPath.LoadFile();
            decimal exchangeRate = 0;

            foreach (var item in exchangeRates)
            {
                if (item.StartsWith(currency.ToString()))
                {
                    string[] split = item.Split(':');
                    try
                    {
                        exchangeRate = decimal.Parse(split[1]);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return exchangeRate;
        }
    }
}
