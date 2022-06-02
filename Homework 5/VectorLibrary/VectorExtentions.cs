using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLibrary.Utils
{
    public static class VectorExtentions
    {
        public static void Print<T>(this IEnumerable<T> arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item.ToString() + " ");
            }
            Console.WriteLine();
        }

        public static Vector<T> ToVector<T>(this IEnumerable<T> e) where T: IComparable<T>
        {
            Vector<T> result = new Vector<T>();
            foreach (var item in e)
            {
                result.Add(item);
            }
            return result;
        }
    }
}
