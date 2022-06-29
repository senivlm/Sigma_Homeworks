using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Products
{
    public interface IProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }
    }
}
