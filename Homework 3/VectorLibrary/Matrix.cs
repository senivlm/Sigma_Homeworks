using System;
using System.Collections.Generic;
using System.Text;

namespace VectorLibrary
{
    public static class Matrix
    {
        public enum WayOfOutput : byte
        {
            DownUp = 1, // standard
            UpDown = 2,
        }
        public static void Print(this int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public static int[,] CoolMatrix_1(int n, int m)
        {
            if (n <= 0 || m <= 0) return new int[0, 0];

            int[,] result = new int[n, m];
            int count = 1;

            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    result[i, j] = count++;
                }
            }

            return result;
        }

        public static int[,] CoolMatrix_2(int n)
        {
            if (n <= 0) return new int[0, 0];

            int[,] result = new int[n, n];
            int i = 0;
            int j = 0;

            for (int count = 1; count <= n * n;)
            {
                GoUp(result, n, ref i, ref j, ref count);
                GoDown(result, n, ref i, ref j, ref count);
            }
            return result;
        }

        public static int[,] CoolMatrix_2(int n, WayOfOutput wayOfOutput)
        {
            if (n <= 0) return new int[0, 0];

            int[,] result = new int[n, n];
            int i = 0;
            int j = 0;

            if (wayOfOutput == Matrix.WayOfOutput.DownUp)
            {
                for (int count = 1; count <= n * n;)
                {
                    GoDown(result, n, ref i, ref j, ref count);
                    GoUp(result, n, ref i, ref j, ref count);
                }
            }

            else if(wayOfOutput == Matrix.WayOfOutput.UpDown)
            {
                for (int count = 1; count <= n * n;)
                {
                    GoUp(result, n, ref i, ref j, ref count);
                    GoDown(result, n, ref i, ref j, ref count);
                }
            }

            return result;
        }

        private static void GoUp(int[,] result, int n, ref int i, ref int j, ref int count)
        {
            for (; i >= 0 && j < n; j++, i--)
            {
                result[i, j] = count++;
            }

            // Set i and j according to direction
            if (i < 0 && j <= n - 1)
                i = 0;
            if (j == n)
            {
                i = i + 2;
                j--;
            }
        }

        private static void GoDown(int[,] result, int n, ref int i, ref int j, ref int count)
        {
            for (; j >= 0 && i < n; i++, j--)
            {
                result[i, j] = count++;
            }

            // Set i and j according to direction
            if (j < 0 && i <= n - 1)
                j = 0;
            if (i == n)
            {
                j = j + 2;
                i--;
            }
        }

        public static int[,] CoolMatrix_3(int n, int m)
        {
            if (n <= 0 || m <= 0) return new int[0, 0];

            int[,] result = new int[n, m];
            int count = 1;

            int i = 0;
            int j = 0;
            int offset = 0;


            for (; count < n * m; offset++)
            {
                i += offset;
                j += offset;
                // down
                for (; i < n - offset; i++)
                {
                    result[i, j] = count++;
                }

                i--;
                j++;

                // to the right
                for (; j < m - offset; j++)
                {
                    result[i, j] = count++;
                }

                j--;
                i--;

                // up
                for (; i >= 0 + offset; i--)
                {
                    result[i, j] = count++;
                }

                i++;
                j--;

                // to the left
                for (; j > 0 + offset; j--)
                {
                    result[i, j] = count++;
                }
            }

            return result;
        }

        public static int[,] counterClockspiralPrint(int n, int m)
        {
            int iterator, i = 0, j = 0;

            var result = new int[n, m];

            // i - starting row index
            // n - ending row index
            // j - starting column index
            // m - ending column index
            // i - iterator

            // initialize the count
            int count = 0;

            // total number of elements in matrix
            int total = n * m;

            while (i < n && j < m)
            {
                if (count == total)
                    break;

                // Print the first column from
                // the remaining columns
                for (iterator = i; iterator < n; ++iterator)
                {
                    result[iterator, j] = ++count;
                }
                j++;

                if (count == total)
                    break;

                // Print the last row from
                // the remaining rows
                for (iterator = j; iterator < m; ++iterator)
                {
                    result[n - 1, iterator] = ++count;
                }
                n--;

                if (count == total)
                    break;

                // Print the last column from
                // the remaining columns
                if (i < n)
                {
                    for (iterator = n - 1; iterator >= i; --iterator)
                    {
                        result[iterator, m - 1] = ++count;
                    }
                    m--;
                }

                if (count == total)
                    break;

                // Print the first row from
                // the remaining rows
                if (j < m)
                {
                    for (iterator = m - 1; iterator >= j; --iterator)
                    {
                        result[i, iterator] = ++count;
                    }
                    i++;
                }
            }
            return result;
        }
    }
}
