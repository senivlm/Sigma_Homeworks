using MatrixLib;
using System;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 2
            var matrix = new Matrix<int>(4, 4);
            matrix.Pattern = ForEachPattern.Diagonal_UpDown;

            int count = 1;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix[i, j] = count++;
                }
            }

            matrix.Print();

            Console.WriteLine();

            foreach (var item in matrix)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine(); 
            #endregion
        }
    }
}