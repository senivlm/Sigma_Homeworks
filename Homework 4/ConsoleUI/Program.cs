using System;
using VectorLibrary;
using VectorLibrary.Utils;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var v1 = new Vector<int>() { 22, 1, 1, 3, 14, 1, 4, 3, 5, 4 };
                v1.Print();

                v1.QuickSort(QuickSortPivot.First);
                v1.QuickSort(QuickSortPivot.Last);
                v1.QuickSort(QuickSortPivot.Middle);
                v1.QuickSort(x => x/10);
                v1.QuickSort(x => x / (43324*3456));

                v1.Print();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}