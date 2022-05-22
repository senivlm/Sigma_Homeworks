using System;
using VectorLibrary;
using VectorLibrary.Utils;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Додати в клас Vector метод, який перевіряє, чи поле є паліндромом.");
            var v1 = new Vector<int>() { 1,2,3,4,5};
            Console.WriteLine(v1.IsPalindrome());
            var v2 = new Vector<string>() { "hello", "world", "world", "hello"};
            Console.WriteLine(v2.IsPalindrome());

            Console.WriteLine("2. Додати в клас Vector метод, який реверсує елементи масиву. Створити власний метод.");
            Console.WriteLine("А також показати використання стандартного методу.");
            v1.Reverse();
            v1.Print();

            int[] array = v1.ToArray().Reverse().ToArray();
            array.Print();
            Array.Reverse(array);
            array.Print();

            Console.WriteLine("3. Додати в клас Vector метод, який в масиві знаходить  найдовшу підпослідовність однакових чисел.");
            var v3 = new Vector<int>() { 1,1,1,1,1,
                                        2,1,2,2,2,3,
                                        4,4,4,4,4,4,4,5};
            v3.GetLongestSequence().Print();

            Console.WriteLine(@"4. У класі Matrix створити метод, який заповнює квадратну матрицю діагональною змійкою, 
параметром методу має бути напрям початкового повороту змійки (вправо, чи вниз), 
заданий  змінною типу Enum.");

            Console.WriteLine();
            Matrix.CoolMatrix_2(5,Matrix.WayOfOutput.DownUp).Print();
            Console.WriteLine();
            Matrix.CoolMatrix_2(5, Matrix.WayOfOutput.UpDown).Print();

            Console.WriteLine("5. Оптимізувати метод InitShufle класу Vector, створений на занятті.");
            var v4 = Vector<int>.InitShuffle(5);
            v4.Print();
            v4 = Vector<int>.InitShuffle(5);
            v4.Print();
            v4 = Vector<int>.InitShuffle(5);
            v4.Print();
        }
    }
}