using System;
using VectorLibrary;
using VectorLibrary.Utils;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var v1 = new Vector<int>() { 22, 1,1, 3, 14,1,4,4};
            v1.Print();
            v1.QuickSort();
            v1.Print();
        }
    }
}