using ShopLib.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Storage
{
    public partial class Storage<T> : IDictionary<T, int> where T : IProduct
    {

        #region IDictionary
        public int this[T key] { get => ((IDictionary<T, int>)_productsStock)[key]; set => ((IDictionary<T, int>)_productsStock)[key] = value; }

        public ICollection<T> Keys => ((IDictionary<T, int>)_productsStock).Keys;

        public ICollection<int> Values => ((IDictionary<T, int>)_productsStock).Values;

        public int Count => ((ICollection<KeyValuePair<T, int>>)_productsStock).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<T, int>>)_productsStock).IsReadOnly;

        public void Add(T key, int value)
        {
            ((IDictionary<T, int>)_productsStock).Add(key, value);
            CheckIfExpired(key);
        }

        public void Add(KeyValuePair<T, int> item)
        {
            ((ICollection<KeyValuePair<T, int>>)_productsStock).Add(item);
            CheckIfExpired(item.Key);
        }

        public void Add(T item)
        {
            _productsStock.Add(item, 0);
            CheckIfExpired(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<T, int>>)_productsStock).Clear();
        }

        public bool Contains(KeyValuePair<T, int> item)
        {
            return ((ICollection<KeyValuePair<T, int>>)_productsStock).Contains(item);
        }

        public bool ContainsKey(T key)
        {
            return ((IDictionary<T, int>)_productsStock).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<T, int>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<T, int>>)_productsStock).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<T, int>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<T, int>>)_productsStock).GetEnumerator();
        }

        public bool Remove(T key)
        {
            return ((IDictionary<T, int>)_productsStock).Remove(key);
        }

        public bool Remove(KeyValuePair<T, int> item)
        {
            return ((ICollection<KeyValuePair<T, int>>)_productsStock).Remove(item);
        }

        public bool TryGetValue(T key, out int value)
        {
            return ((IDictionary<T, int>)_productsStock).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_productsStock).GetEnumerator();
        } 
        #endregion

        private void CheckIfExpired(T item)
        {
            if (item is IExpirable)
            {
                var prod = item as IExpirable;

                if (prod.IsExpired())
                    OnAddingExpired(this, new OnAddingExpiredEventArgs(new KeyValuePair<IExpirable, int>(prod, 0)));
            }
        }
    }
}
