using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ShopLib.Storage
{
    public partial class Storage : IEnumerable<ProductStock>, IList<ProductStock>
    {
        #region IList
        public int Count => _products.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public ProductStock this[int index]
        {
            get => _products[index];
            set => _products[index] = value;
        }

        public int IndexOf(ProductStock item)
        {
            return _products.IndexOf(item);
        }

        public void Insert(int index, ProductStock item)
        {
            _products.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _products.RemoveAt(index);
        }

        public void Add(ProductStock item)
        {
            _products.Add(item);
        }

        public void Clear()
        {
            _products.Clear();
        }

        public bool Contains(ProductStock item)
        {
            return _products.Contains(item);
        }

        public void CopyTo(ProductStock[] array, int arrayIndex)
        {
            _products.CopyTo(array, arrayIndex);
        }

        public bool Remove(ProductStock item)
        {
            return _products.Remove(item);
        }
        #endregion

        #region IEnumerator
        public IEnumerator<ProductStock> GetEnumerator()
        {
            foreach (var item in _products)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
