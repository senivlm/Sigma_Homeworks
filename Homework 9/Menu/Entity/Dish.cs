using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MenuLib.Entity
{
    public class Dish //: IDictionary<Ingredient, double>
    {
        private Dictionary<Ingredient, double> _ingredients = new Dictionary<Ingredient, double>();

        public string Name { get; set; }


        public Dish()
        {
        }
        public Dish(string name)
        {
            Name = name;
        }


        #region IDictionary
        public double this[Ingredient key]
        {
            get { return _ingredients[key]; }
            set { _ingredients[key] = value; }
        }

        public ICollection<Ingredient> Ingredients => _ingredients.Keys;

        public ICollection<double> Weights => _ingredients.Values;

        public int Count => _ingredients.Count;

        //public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Ingredient ingredient, double weight)
        {
            _ingredients.Add(ingredient, weight);
        }

        //public void Add(KeyValuePair<Ingredient, double> item)
        //{
        //    _ingredients.Add(item);
        //}


        public IEnumerator<KeyValuePair<Ingredient, double>> GetEnumerator()
        {
            foreach (var item in _ingredients)
                yield return item;
        }

        //public void Clear()
        //{
        //    _ingredients.Clear();
        //}

        //public bool Contains(KeyValuePair<Ingredient, double> item)
        //{
        //    return _ingredients.ContainsKey(item.Key);
        //}

        //public bool ContainsKey(Ingredient key)
        //{
        //    return _ingredients.ContainsKey(key);
        //}

        //public void CopyTo(KeyValuePair<Ingredient, double>[] array, int arrayIndex)
        //{
        //    throw new NotImplementedException();
        //}


        //public bool Remove(Ingredient key)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Remove(KeyValuePair<Ingredient, double> item)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool TryGetValue(Ingredient key, out double value)
        //{
        //    throw new NotImplementedException();
        //}


        #endregion
    }
}
