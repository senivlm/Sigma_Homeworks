using ShopLib.Products;
using ShopLib.Products.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShopLib.Storage.Interface
{
    public interface IStorage<T> : ISerializable , IList<T> where T : IProduct
    {
    }
}
