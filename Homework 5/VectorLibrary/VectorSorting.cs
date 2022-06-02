using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLibrary
{
    public partial class Vector<T> : IEnumerable<T> where T : IComparable<T>
    {
        #region QuickSort
        public void QuickSort()
        {
            //_array = QuickSortArray_First(_array, Count);
            //_array = QuickSortArray_Last(_array, Count);
            _array = QuickSortArray_Middle(_array, Count);
            //QuickSortArray(_array, 0, Count-1);
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

        private T[] QuickSortArray_Last(T[] array, int length)
        {
            T pivot = array[length - 1];
            T[] less = new T[0];
            T[] greater = new T[0];

            for (int i = length - 2; i >= 0; i--)
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
            T pivot = array[length / 2];
            T[] less = new T[0];
            T[] greater = new T[0];

            for (int i = 0; i < length / 2; i++)
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
            else // array[i] >= pivot
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
        #endregion

        #region PyramidSort
        public void PyramidSort()
        {
            PyramidSortArray(_array, Count);
        }

        private void PyramidSortArray(T[] array, int length)
        {
            BuildMaxHeap(array, length);
            for (int i = length - 1; i >= 0; i--)
            {
                T temp = array[0];
                array[0] = array[i];
                array[i] = temp;
                length--;
                Heapify_2(array, 0, length);
            }
        }

        private void BuildMaxHeap(T[] array, int length)
        {
            for (int i = length / 2 - 1; i >= 0; i--)
            {
                Heapify(array, i, length);
            }
        }

        private void Heapify(T[] array, int i, int length)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            int max;

            if(left < length && array[left].CompareTo(array[i]) > 0)
            {
                max = left;
            }
            else
            {
                max = i;
            }

            if(right < length && array[right].CompareTo(array[max]) > 0)
            {
                max = right;
            }

            if(max != i)
            {
                T temp = array[i];
                array[i] = array[max];
                array[max] = temp;
                Heapify(array, max, length);
            }
        }

        private void Heapify_2(T[] array, int i, int length)
        {
            int left = i + 1;
            int right = i + 2;

            int max;

            if (left < length && array[left].CompareTo(array[i]) > 0)
            {
                max = left;
            }
            else
            {
                max = i;
            }

            if (right < length && array[right].CompareTo(array[max]) > 0)
            {
                max = right;
            }

            if (max != i)
            {
                T temp = array[i];
                array[i] = array[max];
                array[max] = temp;
                Heapify(array, max, length);
            }
        }
        #endregion

        #region MergeSort
        
        public void MergeSort()
        {
            MergeSortArray(_array, Count);
        }

        private void MergeSortArray(T[] array, int length)
        {
            if (length == 1) return;

            // divide the array in two
            int middle = length / 2;
            T[] leftArray = new T[middle];
            T[] rightArray = new T[length - middle];

            Array.Copy(array, 0, leftArray, 0, middle);
            Array.Copy(array, middle, rightArray, 0, length - middle);

            MergeSortArray(leftArray, leftArray.Length);
            MergeSortArray(rightArray, rightArray.Length);
            Merge(leftArray, rightArray, array);
        }

        private void Merge(T[] leftArray, T[] rightArray, T[] array)
        {
            int i = 0; // main array index
            int l = 0; // left array index
            int r = 0; // right array index

            while(l < leftArray.Length && r < rightArray.Length)
            {
                if(leftArray[l].CompareTo(rightArray[r]) < 0) // leftArray[l] < rightArray[r]
                {
                    array[i] = leftArray[l];
                    i++;
                    l++;
                }
                else // leftArray[l] >= rightArray[r]
                {
                    array[i] = rightArray[r];
                    i++;
                    r++;
                }
            }
            while(l < leftArray.Length)
            {
                array[i] = leftArray[l];
                i++;
                l++;
            }
            while(r < rightArray.Length)
            {
                array[i] = rightArray[r];
                i++;
                r++;
            }
        }
        #endregion

        private void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b; 
            b = temp;
        }
    }
}
