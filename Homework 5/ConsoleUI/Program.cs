using System;
using VectorLibrary;
using VectorLibrary.Utils;
using FileSort;

namespace ConsoleUI 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vector<int> vector = new Vector<int>() { 2, 6, 5, 3, 8, 7, 1, 0 };
            vector.Print();
            vector.PyramidSort();
            vector.Print();

            string sortfile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 5\10.txt";
            FileSorter.SortFile(sortfile, 2, ',');
        }

    }
}