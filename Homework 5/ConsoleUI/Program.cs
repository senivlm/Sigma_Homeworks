using System;
using VectorLibrary;
using VectorLibrary.Utils;

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

            // this doesn't work 
            FileSorter sorter = new FileSorter();
            sorter.SortFile(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 5\10.txt", ',');
        }


    }
}