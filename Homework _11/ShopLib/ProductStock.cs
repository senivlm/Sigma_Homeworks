using ShopLib.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib
{
    public class ProductStock
    {
        public IProduct Product { get; set; }

        public int Quantity { get; set; }
    }
}
