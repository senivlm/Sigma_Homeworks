using MenuLib.Entity;
using MenuLib.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MenuLib.Service
{
    public static class MenuService
    {
        internal static List<Dish> GetFromFile(string filePath)
        {
            List <Dish> dishes = new List <Dish>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    Dish dish = new Dish();

                    dish.Name = sr.ReadLine();

                    string line;
                    while ((line = sr.ReadLine()) != "")
                    {
                        string[] split = line.Split(',');

                        if (split.Length != 2)
                            throw new InvalidDataException($"Could not parse line '{line}'");


                        Ingredient ingredient = new Ingredient(split[0]);
                        double weight;
                        try
                        {
                            weight = double.Parse(split[1]);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidDataException($"Couldn't parse data on line '{line}'\nError: {ex.Message}");
                        }

                        dish.Add(ingredient, weight);
                    }

                    dishes.Add(dish);
                }
            }

            return dishes;
        }

        public static decimal GetFullPrice(this Menu menu, IngredientPrices ingredientPrices)
        {
            decimal totalPrice = 0;

            foreach (var dish in menu)
            {
                foreach (var pair in dish)
                {
                    if (ingredientPrices.ContainsKey(pair.Key))
                    {
                        totalPrice += (decimal)(pair.Value) * ingredientPrices[pair.Key] * 0.001m;
                    }
                    else
                    {
                        throw new NoIngredientPriceException($"Ingredient price was not found. ({pair.Key})");
                    }
                }
            }

            return totalPrice;
        }
    }
}
