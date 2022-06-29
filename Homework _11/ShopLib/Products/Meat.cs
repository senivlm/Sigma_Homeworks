using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Products
{
    public class Meat : IProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }

        public MeatCategory Category { get; set; }

        public MeatQuality Quality { get; set; }
    }


    public enum MeatQuality : byte
    {
        First = 1,
        Second = 2,
    }

    public enum MeatCategory : byte
    {
        Lamb = 1,
        Veal = 2,
        Pork = 3,
        Chicken = 4
    }
}
