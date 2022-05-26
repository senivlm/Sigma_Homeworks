using ProductLibrary.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary
{
    public class Storage
    {
        private List<Product> _products;
        public List<Product> Products { get; }

        public Storage()
        {
            _products = new List<Product>();
        }

        public Storage(List<Product> products)
        {
            if(products != null)
                _products = products;
        }

        public IEnumerable<Meat> GetAllMeat()
        {
            List<Meat> result = new List<Meat>();

            foreach (var product in _products)
            {
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

            foreach (var product in _products)
            {
                product.PercentagePrice(percentage);
            }
        }

        public Product this[int index]
        {
            get { return _products[index]; }
            set { _products[index] = value; }
        }
        
    }
}
