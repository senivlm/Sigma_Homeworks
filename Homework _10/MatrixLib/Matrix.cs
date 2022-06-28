using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MatrixLib
{
    public class Matrix<T> : IEnumerable<T>
    {
        private T[,] _matrix;
        public int Rows { get; }
        public int Columns { get; }

        public ForEachPattern Pattern { get; set; } = ForEachPattern.Horizontal;

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            _matrix = new T[Rows, Columns];
        }

        public T this[int i, int j]
        {
            get => _matrix[i,j];
            set => _matrix[i,j] = value;
        }
        public IEnumerator<T> GetEnumerator()
        {// Покажіть цю ідею в групі
            switch (Pattern)
            {
                case ForEachPattern.Horizontal:
                    return ForEach<T>.Horizontal(_matrix);
                    break;

                case ForEachPattern.Horizontal_Alternately:
                    return ForEach<T>.Horizontal_Alternately(_matrix);
                    break;

                case ForEachPattern.Vertical:
                    return ForEach<T>.Vertical(_matrix); 
                    break;

                case ForEachPattern.Diagonal_UpDown:
                    return ForEach<T>.Diagonal_UpDown(_matrix);
                    break;

                case ForEachPattern.Diagonal_DownUp:
                    return ForEach<T>.Diagonal_DownUp(_matrix);
                    break;
            }

            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Print()
        {
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    Console.Write(_matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
