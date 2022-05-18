using ConsoleUI;
using ProductLibrary;
using ProductLibrary.Products;
using ProductLibrary.Receipt;
using System;

namespace Program {
    class Programm
    {
        static void Main()
        {
            try
            {
                ReceiptDemo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        static void MatrixDemo()
        {
            var matrix = CoolMatrixes.counterClockspiralPrint(5, 6);
            matrix.PrintMatrix();
        }

        static void ReceiptDemo()
        {
            var receipt = new Receipt();
            List<Product> products = GetProducts();
            Random random = new Random();

            foreach (var p in products)
            {
                receipt.Add(new ReceiptItem(p, random.Next(1,20)));
            }

            //receipt.TotalPrice.Print();
            receipt.Print();

            receipt.ChangePricesDueToExpiration();

            //receipt.TotalPrice.Print();
            //receipt.Print();

        }

        static void StorageDemo()
        {
            Storage storage = new Storage() { Products = GetProducts() };
            storage.Print();
            List<Meat> meats = storage.GetAllMeat().ToList();
            storage.InitializeManually();
        }

        static List<Product> GetProducts()
        {
            var products = new List<Product>();

            Meat meat1 = new Meat("meat1", 1, 1, Meat.MeatQuality.First, Meat.MeatCategory.Pork);
            Meat meat2 = new Meat("meat2", 1, 1, Meat.MeatQuality.Second, Meat.MeatCategory.Veal);
            //Meat meat3 = new Meat();

            DiaryProduct diary1 = new DiaryProduct("Milk", 1, 1, DateTime.Today.AddDays(-2));
            DiaryProduct diary2 = new DiaryProduct("Cheese", 1, 1, DateTime.Today);

            products.Add(meat1);
            products.Add(meat2);
            //products.Add(meat2);
            products.Add(diary1);
            products.Add(diary2);

            return products;
        }
    }
}
