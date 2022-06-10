using ProductLibrary.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary.Storage
{
    public partial class Storage : IEnumerable<Product>, IList<Product>
    {
        private List<Product> _products;

        public Storage()
        {
            _products = new List<Product>();
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
                product.Price *= percentage;
            }
        }
    }
}
