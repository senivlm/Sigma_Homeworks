using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorLibrary.Utils;

namespace VectorLibrary
{
    public partial class Vector<T> : IEnumerable<T> where T : IComparable<T>
    {
        private T[] _array;

        public int Count { get; set; }
        public int Capacity { get; set; }

        public Vector()
        {
            Capacity = 4;
            _array = new T[Capacity];
        }

        public Vector(int capacity)
        {
            Capacity = capacity;
            _array = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index");

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index");

                _array[index] = value;
            }
        }

        public void Add(T item)
        {
            if (Count >= Capacity)
            {
                Capacity *= 2;
                Array.Resize(ref _array, Capacity);
            }

            _array[Count++] = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsPalindrome()
        {
            if (Count % 2 != 0) return false;

            for (int i = 0, j = Count - 1; i < Count / 2; i++, j--)
            {
                if (!_array[i].Equals(_array[j]))
                    return false;
            }
            return true;
        }

        public void Reverse()
        {
            T[] result = new T[Capacity];

            for (int i = 0, j = Count - 1; j >= 0; i++, j--)
            {
                result[i] = _array[j];
            }

            _array = result;
        }

        public IEnumerable<T> GetLongestSequence()
        {
            int finalIndex = 0;
            int finalCount = 0;

            int index = 0;
            int count = 0;
            bool indexIsSet = false;

            for (int i = 0; i < Count - 1; i++)
            {
                // if the next element has the same value
                if (_array[i].Equals(_array[i + 1]))
                {
                    // if it's the first time
                    if (!indexIsSet)
                    {
                        index = i;
                        indexIsSet = true;
                        count = 2;
                    }
                    // if it's not the first time
                    else
                    {
                        count++;
                    }
                }
                // check if the sequence is longer
                else if (count > finalCount)
                {
                    finalIndex = index;
                    finalCount = count;
                    count = 0;
                    indexIsSet = false;
                }
                else
                {
                    count = 0;
                    indexIsSet = false;
                }
            }

            T[] result = new T[finalCount];
            Array.Copy(_array, finalIndex, result, 0, finalCount);

            return result;
        }

        public static Vector<int> InitShuffle(int maxNum)
        {
            Vector<int> result = new Vector<int>(maxNum);

            for (int i = 1; i < maxNum + 1; i++)
                result.Add(i);

            result.Shuffle();

            return result;
        }

        private void Shuffle()
        {
            Random r = new Random();
            int n = Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                T value = _array[k];
                _array[k] = _array[n];
                _array[n] = value;
            }
        }
    }
}
