using ShopLib.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Order
{
    public class Order
    {
        private List<ProductStock> _products;

        public decimal Price { get; set; }

        public double Weight { get; set; }
    }
}
