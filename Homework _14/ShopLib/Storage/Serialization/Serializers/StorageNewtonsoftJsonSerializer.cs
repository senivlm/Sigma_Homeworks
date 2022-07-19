using Newtonsoft.Json;
using ShopLib.Products;
using ShopLib.Products.Interface;
using ShopLib.Storage.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShopLib.Storage.Serialization.Serializers
{
    public class StorageNewtonsoftJsonSerializer<T> : IStorageSerializer<T> where T : IProduct
    {   
        public void Serialize(IStorage<T> storage, string filePath)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                var serializer = new Newtonsoft.Json.JsonSerializer();
                try
                {
                    serializer.Serialize(sw, storage);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public Storage<T> Deserialize(string filePath)
        {
            string text = File.ReadAllText(filePath);

            try
            {
                JsonConvert.DeserializeObject<Storage<T>>(text);
            }
            catch (Exception)
            {

                throw;
            }

            return Storage<T>.Instance;
        }
    }
}
