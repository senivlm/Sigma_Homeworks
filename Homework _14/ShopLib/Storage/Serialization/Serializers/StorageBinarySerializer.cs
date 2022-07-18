using ShopLib.Products;
using ShopLib.Storage.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ShopLib.Storage.Serialization.Serializers
{
    public class StorageBinarySerializer<T> : IStorageSerializer<T> where T : IProduct
    {
        public void Serialize(IStorage<T> storage, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    formatter.Serialize(fs, storage);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public Storage<T> Deserialize(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                var storage = Storage<T>.GetInstance();

                try
                {
                    storage = (Storage<T>)formatter.Deserialize(fs);
                }
                catch (Exception)
                {
                    throw;
                }
                return Storage<T>.Instance;
            }
        }
    }
}
