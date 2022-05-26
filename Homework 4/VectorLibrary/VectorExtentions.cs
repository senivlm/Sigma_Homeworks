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
    }
}
