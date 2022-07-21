using ShopLib.Products;
using ShopLib.Storage;
using ShopLib.Storage.Serialization;
using ShopLib.Storage.Serialization.Serializers;
using System;

namespace ConsoleUI
{
    internal class Program
    {// Сподобався загалом варіант Критики чистого розуму. Пропоную обговорити на занятті. Але в діаграмі Ви зупинились на  реалізації тільки невеликої частини, а саме товарів. А де вся інша частина хоча б концептуально?
        //І не вже не буде паттернів?
        static void Main(string[] args)
        {// А чому все це щастя в main?
            #region Task 2
            string jsonFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework _14\ConsoleUI\storage_1.json";
            string xmlFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework _14\ConsoleUI\storage_1.xml";
            string datFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework _14\ConsoleUI\storage_1.dat";

            Meat meat1 = new Meat() { Name = "meat1", Category = Meat.MeatCategory.Veal, Price = 10.5m, Quality = Meat.MeatQuality.First, Weight = 546 };
            Meat meat2 = new Meat() { Name = "meat2", Category = Meat.MeatCategory.Veal, Price = 10.5m, Quality = Meat.MeatQuality.First, Weight = 546 };

            var meatStorage = Storage<Meat>.GetInstance();
            //meatStorage.OnAddingExpired += poof;

            meatStorage.Add(meat1);

            var factory = new SerializerFactory();

            // newtonsoft json
            var jsonserializer = factory.CreateInstance<Meat>("StorageNewtonsoftJsonSerializer");
            jsonserializer.Serialize(meatStorage, jsonFile);
            meatStorage.Add(meat2);
            var jsonStorage = jsonserializer.Deserialize(jsonFile);

            // xml
            var xmlserializer = factory.CreateInstance<Meat>("StorageXmlSerializer");
            xmlserializer.Serialize(meatStorage, xmlFile);
            meatStorage.Add(meat2);
            var xmlStorage = xmlserializer.Deserialize(xmlFile);

            // binary
            var binaryserializer = factory.CreateInstance<Meat>("StorageBinarySerializer");
            binaryserializer.Serialize(meatStorage, datFile);
            meatStorage.Add(meat2);
            var binaryStorage = binaryserializer.Deserialize(datFile);

            
            #endregion

            #region Task 3
            // singleton працює 
            Console.WriteLine(meatStorage.Equals(jsonStorage)); // true
            Console.WriteLine(meatStorage.Equals(xmlStorage)); // true
            Console.WriteLine(meatStorage.Equals(binaryStorage)); // true 

            // не зрозуміло на якому рівні абстракції зробити абстрактну фабрику
            // діаграма класів додана до папки завдання 
            #endregion
        }

        private static void poof(object? sender, OnAddingExpiredEventArgs e)
        {
            Console.WriteLine("poof");
        }
    }
}
