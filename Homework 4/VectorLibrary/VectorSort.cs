using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLibrary
{
    public partial class Vector<T> : IEnumerable<T> where T : IComparable<T>
    {
        public void QuickSort(QuickSortPivot pivot)
        {
            switch (pivot)
            {
                case QuickSortPivot.First:
                    _array = QuickSort(_array, Count, x => x - x);
                    break;
                case QuickSortPivot.Last:
                    _array = QuickSort(_array, Count, x => x - 1);
                    break ;
                case QuickSortPivot.Middle:
                    _array = QuickSort(_array, Count, x => x/2);
                    break;
            }
        }

        public void QuickSort(Func<int, int> pivotPredicate)
        {
            _array = QuickSort(_array, Count, pivotPredicate);
        }

        private List<T> QuickSort(List<T> array, int length, Func<int,int> pivotPredicate)
        {
            int pivotIndex = pivotPredicate(length);
            T pivot = array[pivotIndex];
            var less = new List<T>();
            var greater = new List<T>();

            for (int i = 0; i < pivotIndex; i++)
            {
                Compare(array, i, less, greater, pivot);
            }
            for (int i = pivotIndex+1; i < length; i++)
            {
                Compare(array, i, less, greater, pivot);
            }

            if (less.Count > 1)
                less = QuickSort(less, less.Count, pivotPredicate);
            if (greater.Count > 1)
                greater = QuickSort(greater, greater.Count, pivotPredicate);

            // less + pivot + greater
            return Concat(less, pivot, greater);
        }
        private void Compare(List<T> array, int i, List<T> less, List<T> greater, T pivot)
        {
            if (array[i].CompareTo(pivot) < 0) // array[i] < pivot
            {
                less.Add(array[i]);
            }
            else if (array[i].CompareTo(pivot) >= 0) // array[i] >= pivot
            {
                greater.Add(array[i]);
            }
        }

        private List<T> Concat(List<T> less, T pivot, List<T> greater)
        {
            // less + pivot + greater
            List<T> result = new List<T>();
            result.AddRange(less);
            result.Add(pivot);
            result.AddRange(greater);
            return result;
        }
    }
}
