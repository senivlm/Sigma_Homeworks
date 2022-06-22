using MenuLib.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MenuLib.Entity
{
    public class IngredientPrices : IDictionary<Ingredient, decimal>
    {

        private static Dictionary<Ingredient, decimal> _pairs = new Dictionary<Ingredient, decimal>();

        public IngredientPrices()
        {

        }

        public IngredientPrices(string filePath)
        {
            try
            {
                _pairs = PricesService.GetFromFile(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region IDictionary
        public decimal this[Ingredient key] 
        {
            get => _pairs[key];
            set => _pairs[key] = value;
        }

        public ICollection<Ingredient> Keys => _pairs.Keys;

        public ICollection<decimal> Values => _pairs.Values;

        public int Count => _pairs.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Ingredient key, decimal value)
        {
            _pairs.Add(key, value);
        }

        public void Add(KeyValuePair<Ingredient, decimal> item)
        {
            _pairs.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _pairs.Clear();
        }
        public bool ContainsKey(Ingredient key)
        {
            return _pairs.ContainsKey(key);
        }
        public bool Remove(Ingredient key)
        {
            return _pairs.Remove(key);
        }
        public bool TryGetValue(Ingredient key, out decimal value)
        {
            return _pairs.TryGetValue(key, out value);
        }
        public bool Contains(KeyValuePair<Ingredient, decimal> item)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(KeyValuePair<Ingredient, decimal>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(KeyValuePair<Ingredient, decimal> item)
        {
            throw new NotImplementedException();
        }
        public IEnumerator<KeyValuePair<Ingredient, decimal>> GetEnumerator()
        {
            foreach (var item in _pairs)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion
    }
}
