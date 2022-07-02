using ShopLib.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Storage
{
    public partial class Storage : IEnumerable<ProductStock>, IList<ProductStock>
    {
        private List<ProductStock> _products;

        #region Constructors
        public Storage()
        {
            _products = new List<ProductStock>();
        }

        public Storage(int capacity)
        {
            _products = new List<ProductStock>(capacity);
        }

        public Storage(Storage other)
        {
            _products = new List<ProductStock>(other._products);
        }
        #endregion

        #region Methods
        // a) Товари є в першому складі і немає в другому.
        public Storage ExclusiveLeftJoin(Storage other)
        {
            Storage result = new Storage();
            foreach (var stock in _products)
            {
                if (!other._products.Contains(stock))
                {
                    var newStock = new ProductStock((IProduct)stock.Product.Clone(), stock.Quantity);
                    result.Add(newStock);
                }
            }
            return result;
        }

        // b) Товари, які  є спільними в обох складах.
        public Storage InnerJoin(Storage other)
        {
            Storage result = new Storage();
            foreach (var stock in _products)
            {
                if (other._products.Contains(stock))
                {
                    var newStock = new ProductStock((IProduct)stock.Product.Clone(), stock.Quantity);
                    result.Add(newStock);
                }
            }
            return result;
        }

        // c) Спільний список товарів, які є на обох складах, без повторів елементів.
        public Storage FullJoinDistinct(Storage other)
        {
            Storage result = new Storage();
            foreach (var stock in this._products)
            {
                if (!result._products.Contains(stock))
                {
                    var newStock = new ProductStock((IProduct)stock.Product.Clone(), stock.Quantity);
                    result.Add(newStock);
                }
            }
            foreach (var stock in other._products)
            {
                if (!result._products.Contains(stock))
                {
                    var newStock = new ProductStock((IProduct)stock.Product.Clone(), stock.Quantity);
                    result.Add(newStock);
                }
            }
            return result;
        } 
        #endregion
    }
}
