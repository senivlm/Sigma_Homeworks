using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VectorLibrary.Utils;

namespace VectorLibrary
{
    public class Vector<T> : IEnumerable<T> where T : IComparable<T>
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

        public void QuickSort()
        {
            //_array = QuickSortArray_First(_array, Count);
            //_array = QuickSortArray_Last(_array, Count);
            _array = QuickSortArray_Middle(_array, Count);
        }
        private T[] QuickSortArray_First(T[] array, int length)
        {
            T pivot = array[0];
            T[] less = new T[0];
            T[] greater = new T[0];

            for (int i = 1; i < length; i++)
            {
                QuickSort_Compare(ref array, i, ref less, ref greater, pivot);
            }

            if (less.Length > 1)
                less = QuickSortArray_First(less, less.Length);
            if (greater.Length > 1)
                greater = QuickSortArray_First(greater, greater.Length);

            // less + pivot + greater
            return QuickSort_Concat(less, pivot, greater);
        }
// Найчастіше сортують вхідний параметр. А у нашому випадку поле. Крім тогокраще реалізовувати одинметод і передавати йому параметром індекс початкового опорного елемента.
// а Вашу реалізацію треба б помістити в інший клас як статичний метод.
        private T[] QuickSortArray_Last(T[] array, int length)
        {
            T pivot = array[length-1];
            T[] less = new T[0];
            T[] greater = new T[0];

            for (int i = length-2; i >= 0; i--)
            {
                QuickSort_Compare(ref array, i, ref less, ref greater, pivot);
            }

            if (less.Length > 1)
                less = QuickSortArray_Last(less, less.Length);
            if (greater.Length > 1)
                greater = QuickSortArray_Last(greater, greater.Length);

            // less + pivot + greater
            return QuickSort_Concat(less, pivot, greater);
        }

        private T[] QuickSortArray_Middle(T[] array, int length)
        {
            T pivot = array[length/2];
            T[] less = new T[0];
            T[] greater = new T[0];

            for (int i = 0; i < length/2; i++)
            {
                QuickSort_Compare(ref array, i, ref less, ref greater, pivot);
            }
            for (int i = length / 2 + 1; i < length; i++)
            {
                QuickSort_Compare(ref array, i, ref less, ref greater, pivot);
            }

            if (less.Length > 1)
                less = QuickSortArray_Middle(less, less.Length);
            if (greater.Length > 1)
                greater = QuickSortArray_Middle(greater, greater.Length);

            // less + pivot + greater
            return QuickSort_Concat(less, pivot, greater);
        }

        private void QuickSort_Compare(ref T[] array, int i, ref T[] less, ref T[] greater, T pivot)
        {
            if (array[i].CompareTo(pivot) < 0) // array[i] < pivot
            {
                //less.Add(array[i]);
                Array.Resize(ref less, less.Length + 1);
                less[less.Length - 1] = array[i];
            }
            else if (array[i].CompareTo(pivot) >= 0) // array[i] >= pivot
            {
                //greater.Add(array[i]);
                Array.Resize(ref greater, greater.Length + 1);
                greater[greater.Length - 1] = array[i];
            }
        }

        private T[] QuickSort_Concat(T[] less, T pivot, T[] greater)
        {
            // less + pivot + greater
            var result = new T[less.Length + 1 + greater.Length];
            less.CopyTo(result, 0);
            result[less.Length] = pivot;
            greater.CopyTo(result, less.Length + 1);

            return result;
        }
    }
}
