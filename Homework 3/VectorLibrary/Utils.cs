using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLibrary.Utils
{
    public static class Utils
    {
        public static void Print<T>(this IEnumerable<T> arr)
        {
            foreach (var item in arr)
            {
                Console.Write(item.ToString() + " ");
            }
            Console.WriteLine();
        }

        //private static void Shuffle<T>(this Vector<T> list)
        //{
        //    Random rng = new Random();
        //    int n = list.Count;
        //    while (n > 1)
        //    {
        //        n--;
        //        int k = rng.Next(n + 1);
        //        T value = list[k];
        //        list[k] = list[n];
        //        list[n] = value;
        //    }
        //}
    }
}
