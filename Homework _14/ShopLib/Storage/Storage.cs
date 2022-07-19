using Newtonsoft.Json;
using ShopLib.Products;
using ShopLib.Products.Interface;
using ShopLib.Storage.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShopLib.Storage
{
    [Serializable]
    public class Storage<T> : IStorage<T> where T : IProduct
    {
        private List<T> _products = new List<T>();
        public event EventHandler<OnAddingExpiredEventArgs> OnAddingExpired;

        #region Singleton
        private static Storage<T> _instance;
        private Storage()
        {
        }

        private Storage(SerializationInfo info, StreamingContext context)
        {
            // створює новий об'єкт, але долучається до сінглтона
            // singleton працює
            
            _instance._products = (List<T>)info.GetValue("Products", typeof(List<T>));

            // створює новий об'єкт
            // singleton не працює
            // тому у серіалізаторах завжди повертається Storage<T>.Instance

            //_products = (List<T>)info.GetValue("Products", typeof(List<T>));
        }

        [JsonConstructor]
        private Storage(List<T> products)
        {
            _instance._products = products;
        }

        public static Storage<T> Instance => GetInstance();

        public static Storage<T> GetInstance()
        {
            if( _instance == null )
                _instance = new Storage<T>();
            return _instance;
        }
        #endregion

        #region IList
        public T this[int index] { get => _products[index]; set => _products[index] = value; }
        public int Count => _products.Count;
        public bool IsReadOnly => false;
        public void Add(T item)
        {
            _products.Add(item);
            CheckIfExpired(item);
        }
        public void Clear() => _products.Clear();
        public bool Contains(T item) => _products.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _products.CopyTo(array, arrayIndex);
        public int IndexOf(T item) => _products.IndexOf(item);
        public void Insert(int index, T item) => _products.Insert(index, item);
        public bool Remove(T item) => _products.Remove(item);
        public void RemoveAt(int index) => _products.RemoveAt(index);
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _products)
                yield return item;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region ISerializable
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Products", _products);
        } 
        #endregion

        #region Private Methods
        private void CheckIfExpired(T item)
        {
            IExpirable prod = item as IExpirable;
            if (prod != null)
            {
                if (prod.IsExpired())
                    OnAddingExpired(this, new OnAddingExpiredEventArgs(prod));
            }
        }
        #endregion
        
    }
}
