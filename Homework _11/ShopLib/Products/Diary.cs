using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Products
{
    public class Diary : IProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
