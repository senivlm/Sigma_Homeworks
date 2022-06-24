using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixLib
{
    internal static class ForEach<T>
    {
        public static IEnumerator<T> Horizontal(T[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    yield return matrix[i, j];
                }
            }
        }

        public static IEnumerator<T> Horizontal_Alternately(T[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < m; j++)
                        yield return matrix[i, j];
                }
                else
                {
                    for (int j = m - 1; j >= 0; j--)
                        yield return matrix[i, j];
                }
            }
        }

        public static IEnumerator<T> Vertical(T[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    yield return matrix[i, j];
                }
            }
        }

        public static IEnumerator<T> Diagonal_UpDown(T[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            if (n != m)
                throw new Exception("N and M have to be equal");

            int i = 0;
            int j = 0;

            for (int count = 1; count <= n * n;)
            {
                for (; i >= 0 && j < n; j++, i--)
                {
                    count++;
                    yield return matrix[i, j];

                }

                // Set i and j according to direction
                if (i < 0 && j <= n - 1)
                    i = 0;
                if (j == n)
                {
                    i = i + 2;
                    j--;
                }

                for (; j >= 0 && i < n; i++, j--)
                {
                    count++;
                    yield return matrix[i, j];
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
        }

        public static IEnumerator<T> Diagonal_DownUp(T[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            if (n != m)
                throw new Exception("N and M have to be equal");

            int i = 0;
            int j = 0;

            for (int count = 1; count <= n * n;)
            {
                for (; j >= 0 && i < n; i++, j--)
                {
                    count++;
                    yield return matrix[i, j];
                }

                // Set i and j according to direction
                if (j < 0 && i <= n - 1)
                    j = 0;
                if (i == n)
                {
                    j = j + 2;
                    i--;
                }

                for (; i >= 0 && j < n; j++, i--)
                {
                    count++;
                    yield return matrix[i, j];

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
        }

        
    }
}
