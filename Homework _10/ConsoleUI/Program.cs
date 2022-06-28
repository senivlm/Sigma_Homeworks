using MatrixLib;
using System;
using TranslatorLib;
using static TranslatorLib.Translator;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Task 1
            string textFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework _10\text.txt";
            string dictFile = @"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework _10\dict.txt";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            List<string> text;
            try
            {
                text = Utils.GetText(textFile);

                Translator translator = new Translator();
                translator.Dictionary = new TranslationDictionary(dictFile);

                if (!translator.Dictionary.IsEmpty())
                {
                    translator.OnTranslationAbsence += Translator_OnTranslationAbsence;
                    var translated = translator.Translate(text, 2);

                    Console.Clear();
                    foreach (var line in translated)
                    {
                        Console.WriteLine(line);
                    }
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            #endregion

            #region Task 2
            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            #endregion
        }






        private static void Translator_OnTranslationAbsence(object? sender, TranslationAbsenceEventArgs e)
        {
            if(sender != null)
            {
                TranslationDictionary dict = sender as TranslationDictionary;

                if (dict != null)
                {
                    Console.Clear();
                    Console.WriteLine("Inputs left: " + e.InputsLeft);
                    Console.WriteLine($"Введiть замiну для слова {e.Word}");
                    string value = Console.ReadLine();
                    dict.Add(e.Word, value);
                    dict.AddToFile(e.Word, value);
                }
            }
            
            if(e.InputsLeft == 1)
            {
                Console.Clear();
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Goodbye \n");

                Console.ForegroundColor = color;
                return;
            }
        }
    }
}