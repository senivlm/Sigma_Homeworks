using ProductLibrary.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ProductLibrary.Storage
{
    public static class FileInput
    {
        public static (Storage, bool) ConvertToStorage(this List<string> lines)
        {
            Storage storage = new Storage();
            bool allParsed = true;

            foreach (var line in lines)
            {
                string[] data = line.Split(Config.FILE_SEPARATOR);
                Product p = null;
                bool parsed = true;

                switch (data[0])
                {
                    case "m":
                        (p, parsed) = CreateMeat(data);
                        break;
                    case "d":
                        (p, parsed) = CreateDiary(data);
                        break;
                    default:
                        Logger.Log(line);
                        // create bool to indicate not all lines were parsed
                        allParsed = false;
                        break;
                }

                // check if the current line has been parsed
                if (!parsed)
                {
                    Logger.Log(line);
                    allParsed = false;
                }

                if (p != null)
                    storage.Add(p);
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
                meat.WeightKg = double.Parse(data[3], cultureInfo);
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
        private static (DiaryProduct, bool) CreateDiary(string[] data)
        {
            DiaryProduct diary = new DiaryProduct();

            CultureInfo cultureInfo = new CultureInfo("en-US");
            TextInfo textInfo = cultureInfo.TextInfo;

            try
            {
                diary.Name = textInfo.ToTitleCase(data[1]);
                diary.Price = decimal.Parse(data[2].Trim(), cultureInfo);
                diary.WeightKg = double.Parse(data[3], cultureInfo);
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
