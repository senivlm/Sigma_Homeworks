using ShopLib.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShopLib.Storage.Input
{
    public static class FileInput
    {
        public static bool ConvertToStorage(this Storage<IProduct> storage, List<string> lines)
        {
            bool allParsed = true;

            foreach (var line in lines)
            {
                string[] data = line.Split(Config.FILE_SEPARATOR);
                IProduct prod = null;
                bool parsed = true;

                switch (data[0])
                {
                    case "m":
                        (prod, parsed) = CreateMeat(data);
                        break;
                    case "d":
                        (prod, parsed) = CreateDiary(data);
                        break;
                    default:
                        ShopLib.Storage.Logger.Logger.Log(line);
                        // create bool to indicate not all lines were parsed
                        allParsed = false;
                        break;
                }

                // check if the current line has been parsed
                if (!parsed)
                {
                    ShopLib.Storage.Logger.Logger.Log(line);
                    allParsed = false;
                }

                if (prod != null)
                    storage.Add(prod);
            }
            return allParsed;
        }
        public static (Storage<IProduct>, bool) ConvertToStorage(this List<string> lines)
        {
            Storage<IProduct> storage = new Storage<IProduct>();
            bool allParsed = true;

            foreach (var line in lines)
            {
                string[] data = line.Split(Config.FILE_SEPARATOR);
                IProduct prod = null;
                bool parsed = true;

                switch (data[0])
                {
                    case "m":
                        (prod, parsed) = CreateMeat(data);
                        break;
                    case "d":
                        (prod, parsed) = CreateDiary(data);
                        break;
                    default:
                        ShopLib.Storage.Logger.Logger.Log(line);
                        // create bool to indicate not all lines were parsed
                        allParsed = false;
                        break;
                }

                // check if the current line has been parsed
                if (!parsed)
                {
                    ShopLib.Storage.Logger.Logger.Log(line);
                    allParsed = false;
                }

                if (prod != null)
                    storage.Add(prod);
            }
            return (storage, allParsed);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>(null, false) if data wasn't parsed</returns>
        private static (Meat, bool) CreateMeat(string[] data)
        {
            Meat meat = new Meat();

            CultureInfo cultureInfo = new CultureInfo("en-US");
            TextInfo textInfo = cultureInfo.TextInfo;

            try
            {
                meat.Name = textInfo.ToTitleCase(data[1]);
                meat.Price = decimal.Parse(data[2], cultureInfo);
                meat.Weight = double.Parse(data[3], cultureInfo);
                meat.Quality = (Meat.MeatQuality)Enum.Parse(typeof(Meat.MeatQuality), textInfo.ToTitleCase(data[4]));
                meat.Category = (Meat.MeatCategory)Enum.Parse(typeof(Meat.MeatCategory), textInfo.ToTitleCase(data[5]));
            }
            catch (Exception)
            {
                return (null, false);
            }

            return (meat, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>(null, false) if data wasn't parsed</returns>
        private static (Diary, bool) CreateDiary(string[] data)
        {
            Diary diary = new Diary();

            CultureInfo cultureInfo = new CultureInfo("en-US");
            TextInfo textInfo = cultureInfo.TextInfo;

            try
            {
                diary.Name = textInfo.ToTitleCase(data[1]);
                diary.Price = decimal.Parse(data[2].Trim(), cultureInfo);
                diary.Weight = double.Parse(data[3], cultureInfo);
                diary.ExpirationDate = DateTime.Parse(data[4]);
            }
            catch (Exception)
            {
                return (null, false);
            }

            return (diary, true);
        }
    }
}
