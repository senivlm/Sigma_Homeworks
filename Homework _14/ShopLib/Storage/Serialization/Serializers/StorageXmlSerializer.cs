using ShopLib.Products;
using ShopLib.Products.Interface;
using ShopLib.Storage.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ShopLib.Storage.Serialization.Serializers
{
    public class StorageXmlSerializer<T> : IStorageSerializer<T> where T : IProduct
    {
        public void Serialize(IStorage<T> storage, string filePath)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Storage<T>));
            using (StreamWriter sw = File.CreateText(filePath))
            {

                try
                {
                    xs.Serialize(sw, storage);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public Storage<T> Deserialize(string filePath)
        {
            using (FileStream fs = File.Open(filePath, FileMode.OpenOrCreate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Storage<T>));

                try
                {
                    serializer.Deserialize(fs);
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
