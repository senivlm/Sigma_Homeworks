using ShopLib.Products;
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
