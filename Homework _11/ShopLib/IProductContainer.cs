using ShopLib.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib
{
    public interface IProductContainer<T> : IDictionary<T, int> where T : IProduct
    {
    }
}
