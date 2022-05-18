using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProductLibrary.Receipt
{
    // Check
    public static class ReceiptHelper
    {
        public static void Print(this Receipt receit)
        {
            if(receit != null)
            {
                foreach (var item in receit)
                {
                    Console.WriteLine($"{item.Product.Name}\n{item.Quantity} X {item.Product.Price}({item.TotalWeight:0.##}kg)\t\t\t{item.TotalPrice.ToString("C", new CultureInfo("en-US"))}\n");
                }
                Console.WriteLine($"Total price:\t\t\t\t{receit.TotalPrice.ToString("C", new CultureInfo("en-US"))}");
                Console.WriteLine($"Total weight:\t\t\t\t{receit.TotalWeight} kg");
            }
        }

        public static void Print(this decimal price)
        {
            Console.WriteLine(price);
        }

        public static void Print(this double weight)
        {
            Console.WriteLine(weight);
        }
    }
}
