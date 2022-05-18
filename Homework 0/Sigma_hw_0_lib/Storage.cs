using ProductLibrary.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary
{
    public class Storage
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public void Print()
        {
            if(Products != null)
            {
                foreach (Product p in Products)
                    Console.WriteLine(p.ToString() + "\n");
            }
        }

        public IEnumerable<Meat> GetAllMeat()
        {
            List<Meat> result = new List<Meat>();

            foreach (var product in Products)
            {
                //Meat meat = product as Meat;

                //if (meat != null)
                //    result.Add(meat);

                if(product is Meat meat)
                {
                    result.Add(meat);
                }
            }
            return result;
        }

        public void PercentageAllPrices(decimal percentage)
        {
            if (percentage == 0) return;

            foreach (var product in Products)
            {
                product.PercentagePrice(percentage);
            }
        }

        public Product this[int index]
        {
            get { return Products[index]; }
            set { Products[index] = value; }
        }
        
    }
}
