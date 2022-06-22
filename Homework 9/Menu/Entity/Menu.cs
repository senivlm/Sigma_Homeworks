using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MenuLib.Entity
{
    public class Menu : IEnumerable<Dish>, IList<Dish>
    {
        private List<Dish> _dishes = new List<Dish>();

        #region ctor
        public Menu()
        {
            _dishes = new List<Dish>();
        }

        public Menu(int capacity)
        {
            _dishes = new List<Dish>(capacity);
        }

        public Menu(IEnumerable<Dish> collection)
        {
            _dishes = new List<Dish>(collection);
        }

        public Menu(string filePath)
        {
            try
            {
                _dishes = MenuLib.Service.MenuService.GetFromFile(filePath);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region IList
        public Dish this[int index]
        {
            get { return _dishes[index]; }
            set { _dishes[index] = value; }
        }

        public int Count { get { return _dishes.Count; } }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Dish item)
        {
            _dishes.Add(item);
        }

        public void Clear()
        {
            _dishes.Clear();
        }

        public bool Contains(Dish item)
        {
            return _dishes.Contains(item);
        }

        public void CopyTo(Dish[] array, int arrayIndex)
        {
            _dishes.CopyTo(array, arrayIndex);
        }



        public int IndexOf(Dish item)
        {
            return _dishes.IndexOf(item);
        }

        public void Insert(int index, Dish item)
        {
            _dishes.Insert(index, item);
        }

        public bool Remove(Dish item)
        {
            return _dishes.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _dishes.RemoveAt(index);
        }

        #endregion

        #region IEnumerable
        public IEnumerator<Dish> GetEnumerator()
        {
            foreach (var ingredient in _dishes)
                yield return ingredient;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } 
        #endregion
    }
}
