using ShopLib.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Storage
{
    public partial class Storage<T> : IProductContainer<T>
    {
        private Dictionary<T, int> _productsStock;

        public event EventHandler<OnAddingExpiredEventArgs> OnAddingExpired;

        #region Constructors
        public Storage()
        {
            _productsStock = new Dictionary<T, int>();
        }

        public Storage(int capacity)
        {
            _productsStock = new Dictionary<T, int>(capacity);
        }

        public Storage(Storage<T> other)
        {
            _productsStock = new Dictionary<T, int>(other);
        }
        #endregion

        #region Methods
        // a) Товари є в першому складі і немає в другому.
        public Storage<T> ExclusiveLeftJoin(Storage<T> other)
        {
            Storage<T> result = new Storage<T>();
            foreach (var prod in _productsStock.Keys)
            {
                if (!other._productsStock.ContainsKey(prod))
                {
                    result.Add((T)prod.Clone());
                }
            }
            return result;
        }

        // b) Товари, які  є спільними в обох складах.
        public Storage<T> InnerJoin(Storage<T> other)
        {
            Storage<T> result = new Storage<T>();
            foreach (var prod in _productsStock.Keys)
            {
                if (other._productsStock.ContainsKey(prod))
                {
                    result.Add((T)prod.Clone());
                }
            }
            return result;
        }

        // c) Спільний список товарів, які є на обох складах, без повторів елементів.
        public Storage<T> FullJoinDistinct(Storage<T> other)
        {
            Storage<T> result = new Storage<T>();
            foreach (var prod in this._productsStock.Keys)
            {
                if (!result._productsStock.ContainsKey(prod))
                {
                    result.Add((T)prod.Clone());
                }
            }
            foreach (var prod in other._productsStock.Keys)
            {
                if (!result._productsStock.ContainsKey(prod))
                {
                    result.Add((T)prod.Clone());
                }
            }
            return result;
        }
        #endregion
    }

    public class OnAddingExpiredEventArgs : EventArgs
    {
        public KeyValuePair<IExpirable, int> ProductStock { get; }
        public OnAddingExpiredEventArgs(KeyValuePair<IExpirable, int> productStock)
        {
            ProductStock = productStock;
        }
        public OnAddingExpiredEventArgs(IExpirable product)
        {
            ProductStock = new KeyValuePair<IExpirable, int>(product, 0);
        }
    }
}
