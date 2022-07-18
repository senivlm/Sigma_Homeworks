using ShopLib.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Order
{
    public partial class Order : IProductContainer<IProduct>
    {
        #region IDictionary
        public int this[IProduct key] { get => _productsStock[key]; set => _productsStock[key] = value; }

        public ICollection<IProduct> Keys => _productsStock.Keys;

        public ICollection<int> Values => _productsStock.Values;

        public int Count => _productsStock.Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<IProduct, int>>)_productsStock).IsReadOnly;

        public void Add(IProduct key, int value)
        {
            _productsStock.Add(key, value);
        }

        public void Add(KeyValuePair<IProduct, int> item)
        {
            ((ICollection<KeyValuePair<IProduct, int>>)_productsStock).Add(item);
        }

        public void Clear()
        {
            _productsStock.Clear();
        }

        public bool Contains(KeyValuePair<IProduct, int> item)
        {
            return ((ICollection<KeyValuePair<IProduct, int>>)_productsStock).Contains(item);
        }

        public bool ContainsKey(IProduct key)
        {
            return _productsStock.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<IProduct, int>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<IProduct, int>>)_productsStock).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<IProduct, int>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<IProduct, int>>)_productsStock).GetEnumerator();
        }

        public bool Remove(IProduct key)
        {
            return _productsStock.Remove(key);
        }

        public bool Remove(KeyValuePair<IProduct, int> item)
        {
            return ((ICollection<KeyValuePair<IProduct, int>>)_productsStock).Remove(item);
        }

        public bool TryGetValue(IProduct key, out int value)
        {
            return _productsStock.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion
    }
}
