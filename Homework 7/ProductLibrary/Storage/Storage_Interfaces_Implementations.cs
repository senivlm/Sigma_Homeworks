using ProductLibrary.Products;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProductLibrary.Storage
{
    public partial class Storage : IEnumerable<Product>, IList<Product>
    {//продемонструвати на занятті застосування інтерфейсів
        #region IList
        public int Count => _products.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public Product this[int index]
        {
            get
            {
                if (index < 0 || index >= _products.Count)
                    throw new ArgumentOutOfRangeException("index");

                return _products[index];
            }
            set
            {
                if (index < 0 || index >= _products.Count)
                    throw new ArgumentOutOfRangeException("index");

                _products[index] = value;
            }
        }

        public int IndexOf(Product item)
        {
            return _products.IndexOf(item);
        }

        public void Insert(int index, Product item)
        {
            _products.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _products.RemoveAt(index);
        }

        public void Add(Product item)
        {
            _products.Add(item);
        }

        public void Clear()
        {
            _products.Clear();
        }

        public bool Contains(Product item)
        {
            return _products.Contains(item);
        }

        public void CopyTo(Product[] array, int arrayIndex)
        {
            _products.CopyTo(array, arrayIndex);
        }

        public bool Remove(Product item)
        {
            return _products.Remove(item);
        } 
        #endregion

        #region IEnumerable
        public IEnumerator<Product> GetEnumerator()
        {
            foreach (var product in _products)
                yield return product;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion
    }
}
