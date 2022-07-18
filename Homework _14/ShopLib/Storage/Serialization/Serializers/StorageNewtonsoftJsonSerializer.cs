using Newtonsoft.Json;
using ShopLib.Products;
using ShopLib.Storage.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShopLib.Storage.Serialization.Serializers
{
    public class StorageNewtonsoftJsonSerializer<T> : IStorageSerializer<T> where T : IProduct
    {   
        public void Serialize(IStorage<T> storage, string jsonPath)
        {
            using (StreamWriter jsonStream = File.CreateText(jsonPath))
            {
                var jss = new Newtonsoft.Json.JsonSerializer();
                try
                {
                    jss.Serialize(jsonStream, storage);
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

            var storage = Storage<T>.GetInstance();
            try
            {
                //storage = JsonConvert.DeserializeObject<Storage<T>>(text, new JsonSerializerSettings
                //{
                //    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                //});

                storage = JsonConvert.DeserializeObject<Storage<T>>(text);
            }
            catch (Exception)
            {

                throw;
            }

            return Storage<T>.Instance;
        }
    }
}
