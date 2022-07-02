using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class Translator
    {
        private Dictionary<string, string> _dictionary;
        private string text;
        private string pathToText;
        private string pathToDictionary;
        private int countVariedle = 3;

        public Translator() : this(@"../../../Text.txt", @"../../../Dictionary.txt")
        {

        }

        public Translator(string pathToText, string pathToDictionary)
        {
            _dictionary = new Dictionary<string, string>();
            text = "";
            this.pathToText = pathToText;
            this.pathToDictionary = pathToDictionary;
        }

        public Translator(Dictionary<string, string> vocabluary, string text, string pathToText, string pathToDictionary)
        {
            this.pathToText = pathToText;
            this.pathToDictionary = pathToDictionary;
            this._dictionary = vocabluary;
            this.text = text;
        }

        public void AddText(string text)
        {
            this.text += text;
        }

        public void AddDictionary(Dictionary<string, string> dictionary)
        {
            this._dictionary = dictionary;
        }

        public string ChangeWords()
        {
            string result = "";
            var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                char temp = ' ';
                string tempWord = "";
                int i = 0;
                if (Char.IsPunctuation(word[word.Length - 1]))
                {
                    temp = word[word.Length - 1];
                    while (!_dictionary.ContainsKey(word[0..^1]) && i < countVariedle)
                    {
                        AddToDictionary(word[0..^1]);
                        i++;
                    }
                    tempWord = _dictionary[word[0..^1]] + temp;
                }
                else
                {
                    while (!_dictionary.ContainsKey(word) && i < countVariedle)
                    {
                        AddToDictionary(word);
                        i++;
                    }
                    tempWord = _dictionary[word];
                }
                result += tempWord + " ";
            }

            return result;
        }

        private void AddToDictionary(string word)
        {
            Console.WriteLine($"Введiть замiну для слова {word}");
            string value = Console.ReadLine();
            _dictionary.Add(word, value);
            Reader.WriteToDictionary(word, value, @"../../../Dictionary.txt");
        }
    }
}
