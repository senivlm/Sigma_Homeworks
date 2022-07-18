using ShopLib.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Storage.Input
{
    public static class ConsoleInput
    {
        /// <summary>
        /// Extention method for manually initializing List<Product> in a Storage class via Console
        /// </summary>
        /// <param name="storage"></param>
        public static void Initialize(this Storage<IProduct> storage)
        {
            bool run = CheckBeforeInit(storage);

            if (run)
            {
                storage.Clear();
                AddProducts(storage);
            }
        }

        public static void AddProducts(Storage<IProduct> storage)
        {
            int category;
            ChooseCategory(out category);

            switch (category)
            {
                // 1
                case 1:
                    bool createMore = false;
                    do
                    {
                        storage.Add(CreateMeat());
                        Console.Clear();
                        Console.WriteLine("New meat product is added");

                        bool inputIsValid = false;
                        do
                        {
                            Console.WriteLine("Add another meat product? (y/n)");
                            createMore = Enter_Yes_or_No(Console.ReadLine(), out inputIsValid);
                        } while (!inputIsValid);

                    } while (createMore);
                    break;
                // 2
                case 2:
                    createMore = false;
                    do
                    {
                        storage.Add(CreateDiary());
                        Console.Clear();
                        Console.WriteLine("New diary product is added");

                        bool inputIsValid = false;
                        do
                        {
                            Console.WriteLine("Add another diary product? (y/n)");
                            createMore = Enter_Yes_or_No(Console.ReadLine(), out inputIsValid);
                        } while (!inputIsValid);

                    } while (createMore);
                    break;
                default:
                    break;
            }
        }

        private static void ChooseCategory(out int category)
        {
            bool inputIsValid = false;

            do
            {
                Console.Clear();
                Console.WriteLine("What product do you want to add? (type in the number)");
                Console.WriteLine("1. Meat");
                Console.WriteLine("2. Diary");

                inputIsValid = int.TryParse(Console.ReadLine(), out category);

                inputIsValid = category switch
                {
                    1 => true,
                    2 => true,
                    _ => false
                };
            } while (!inputIsValid);
        }

        /// <summary>
        /// Asks user if they want to rewrite the storage 
        /// </summary>
        /// <param name="storage"></param>
        /// <returns>true if YES, false if NO</returns>
        private static bool CheckBeforeInit(Storage<IProduct> storage)
        {
            bool result = false;

            if (storage.Count > 0)
            {
                Console.WriteLine("Storage already has data in it.");
                Console.WriteLine("Do you want to rewrite it? (y/n)");

                result = Enter_Yes_or_No(Console.ReadLine());
            }

            return result;
        }

        private static Meat CreateMeat()
        {
            string name;
            decimal price;
            double weightKg;
            Meat.MeatQuality quality;
            Meat.MeatCategory category;

            Console.Clear();

            // name
            Console.Write("Enter Name: ");

            name = Console.ReadLine();

            // price
            bool priceIsValid = false;
            do
            {
                Console.Write("Enter price: ");
                priceIsValid = decimal.TryParse(Console.ReadLine(), out price);
            } while (!priceIsValid);

            // weight
            bool weightIsValid = false;
            do
            {
                Console.Write("Enter weight in kg: ");
                weightIsValid = double.TryParse(Console.ReadLine(), out weightKg);
            } while (!weightIsValid);

            // quality
            bool qualityIsValid = false;
            do
            {
                Console.WriteLine("Enter meat quality number: ");
                Console.WriteLine("1. First");
                Console.WriteLine("2. Second");


                qualityIsValid = Enum.TryParse(Console.ReadLine(), out quality);

                qualityIsValid = quality switch
                {
                    Meat.MeatQuality.First => true,
                    Meat.MeatQuality.Second => true,
                    _ => false
                };
            } while (!qualityIsValid);

            // category
            bool categoryIsValid = false;
            do
            {
                Console.WriteLine("Enter meat type number: ");
                Console.WriteLine("1. Lamb");
                Console.WriteLine("2. Veal");
                Console.WriteLine("3. Pork");
                Console.WriteLine("4. Chicken");


                categoryIsValid = Enum.TryParse(Console.ReadLine(), out category);

                categoryIsValid = category switch
                {
                    Meat.MeatCategory.Lamb => true,
                    Meat.MeatCategory.Veal => true,
                    Meat.MeatCategory.Pork => true,
                    Meat.MeatCategory.Chicken => true,
                    _ => false
                };
            } while (!categoryIsValid);

            var result = new Meat(name, price, weightKg, quality, category);

            return result;
        }

        private static Diary CreateDiary()
        {
            string name;
            decimal price;
            double weightKg;
            DateTime expirationDate;

            Console.Clear();

            // name
            Console.Write("Enter Name: ");

            name = Console.ReadLine();

            // price
            do
            {
                Console.Write("Enter price: ");
            } while (decimal.TryParse(Console.ReadLine(), out price));

            // weight
            do
            {
                Console.Write("Enter weight in kg: ");
            } while (double.TryParse(Console.ReadLine(), out weightKg));

            do
            {
                Console.Write("Enter expiration date: ");
            } while (Enum.TryParse(Console.ReadLine(), out expirationDate));


            var result = new Diary(name, price, weightKg, expirationDate);

            return result;
        }


        private static bool Enter_Yes_or_No(string input, out bool inputIsValid)
        {
            bool result = false;

            inputIsValid = input switch
            {
                "Y" => true,
                "y" => true,
                "N" => true,
                "n" => true,
                _ => false
            };

            if (inputIsValid)
            {
                result = input switch
                {
                    "Y" => true,
                    "y" => true,
                    "N" => false,
                    "n" => false,
                };
            }

            return result;
        }

        private static bool Enter_Yes_or_No(string input)
        {
            bool result = false;

            bool inputIsValid = input switch
            {
                "Y" => true,
                "y" => true,
                "N" => true,
                "n" => true,
                _ => false
            };

            if (inputIsValid)
            {
                result = input switch
                {
                    "Y" => true,
                    "y" => true,
                    "N" => false,
                    "n" => false,
                };
            }

            return result;
        }
    }
}
