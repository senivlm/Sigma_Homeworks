using ShopLib.Products;
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
            using (StreamWriter stream = File.CreateText(filePath))
            {

                try
                {
                    xs.Serialize(stream, storage);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public Storage<T> Deserialize(string filePath)
        {
            using (FileStream stream = File.Open(filePath, FileMode.OpenOrCreate))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Storage<T>));
                var storage = Storage<T>.Instance;

                try
                {
                    storage = (Storage<T>)xs.Deserialize(stream);
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
