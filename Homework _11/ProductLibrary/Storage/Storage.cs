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

        // a) Товари є в першому складі і немає в другому.
        public Storage ExclusiveLeftJoin(Storage other)
        {
            Storage result = new Storage();
            foreach (var product in _products)
            {
                if (!other._products.Contains(product))
                {
                    result.Add((Product)product.Clone());
                }
            }
            return result;
        }

        // b) Товари, які  є спільними в обох складах.
        public Storage InnerJoin(Storage other)
        {
            Storage result = new Storage();
            foreach (var product in _products)
            {
                if (other._products.Contains(product))
                {
                    result.Add((Product)product.Clone());
                }
            }
            return result;
        }

        // c) Спільний список товарів, які є на обох складах, без повторів елементів.
        public Storage FullJoinDistinct(Storage other)
        {
            Storage result = new Storage();
            foreach (var product in this._products)
            {
                if (!result._products.Contains(product))
                {
                    result.Add((Product)product.Clone());
                }
            }
            foreach (var product in other._products)
            {
                if (!result._products.Contains(product))
                {
                    result.Add((Product)product.Clone());
                }
            }
            return result;
        }

        //public Storage FullJoin(Storage other)
        //{
        //    Storage result = new Storage();
        //    //result._products.AddRange(this._products);
        //    foreach (var item in this._products)
        //    {
        //        result._products.Add((Product)item.Clone());
        //    }

        //    //result._products.AddRange(other._products);
        //    foreach (var item in other._products)
        //    {
        //        result._products.Add((Product)item.Clone());
        //    }
        //    return result;
        //}

        //public Storage Distinct()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
