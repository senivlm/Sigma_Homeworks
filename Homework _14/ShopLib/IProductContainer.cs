using ShopLib.Products;
using ShopLib.Products.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib
{
    public interface IProductContainer<T> : IDictionary<T, int> where T : IProduct
    {
    }
}
