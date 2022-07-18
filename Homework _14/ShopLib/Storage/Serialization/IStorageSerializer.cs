using ShopLib.Products;
using ShopLib.Storage.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Storage.Serialization
{
    public interface IStorageSerializer<T> where T : IProduct
    {
        void Serialize(IStorage<T> storage, string filePath);

        Storage<T> Deserialize(string filePath);
    }
}
