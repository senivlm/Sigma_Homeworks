using MenuLib.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MenuLib.Service
{
    public static class PricesService
    {
        internal static Dictionary<Ingredient, decimal> GetFromFile(string filePath)
        {
            Dictionary<Ingredient, decimal> prices = new Dictionary<Ingredient, decimal>();

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] split = line.Split(',');

                    if (split.Length != 2)
                        throw new InvalidDataException($"Could not parse line '{line}'");

                    Ingredient ingredient = new Ingredient(split[0]);
                    decimal price;

                    try
                    {
                        price = decimal.Parse(split[1]);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidDataException($"Couldn't parse data on line '{line}'\nError: {ex.Message}");
                    }


                    if (prices.ContainsKey(ingredient))
                    {
                        prices[ingredient] = price;
                    }
                    else
                    {
                        prices.Add(ingredient, price);
                    }
                }
            }

            return prices;
        }

    }
}
