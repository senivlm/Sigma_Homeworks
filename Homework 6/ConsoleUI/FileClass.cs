using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class FileClass
    {
        public void ReadFile(StreamReader sr)
        {
            string line = sr.ReadToEndAsync().Result;
            line = line.Replace("\r\n", "");
            line = line.Replace("  ", "");
            //line = line.Trim();

            List<string> sentences = GetSentences(line);

            File.WriteAllLines(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\Result.txt", sentences);

            foreach (string sentence in sentences)
            {
                File.AppendAllText(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\Result.txt", "\n");
                File.AppendAllLines(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\Result.txt", GetLongestWords(sentence));
            }
            foreach (string sentence in sentences)
            {
                File.AppendAllText(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\Result.txt", "\n");
                File.AppendAllLines(@"C:\Users\Иван\source\repos\Sigma_Homeworks\Homework 6\Result.txt", GetShortestWords(sentence));
            }

        }

        public List<string> GetSentences(string line)
        {
            List<string> sentences = Regex.Split(line, @"(?<=[.])").ToList(); //line.Split('.').ToList();
            for (int i = 0; i < sentences.Count; i++)
            {
                sentences[i] = sentences[i].Trim();

                if (sentences[i].Length < 1)
                    sentences.Remove(sentences[i]);
            }
            return sentences;
        }

        public (List<string>, List<string>) GetLongestAndShortestWord(string sentence)
        {
            var punctuation = sentence.Where(Char.IsPunctuation).Distinct().ToArray();
            List<string> words = sentence.Split().Select(x => x.Trim(punctuation)).ToList();

            int min = 0;
            int max = 0;
            foreach (var word in words)
            {
                if(word.Length > max)
                    max = word.Length;
                if (word.Length < min)
                    min = word.Length;
            }

            List<string> longest = words.Where(x => x.Length == max).ToList();
            List<string> shortest = words.Where(x => x.Length == min).ToList();

            return (longest, shortest);
        }

        public List<string> GetLongestWords(string sentence)
        {
            //sentence = Regex.Replace(sentence, "[^a-zA-Z0-9% ._]", string.Empty);
            var punctuation = sentence.Where(Char.IsPunctuation).Distinct().ToArray();
            List<string> words = sentence.Split().Select(x => x.Trim(punctuation)).ToList();
            //string[] words = sentence.Split();

            int max = 0;
            foreach (var word in words)
            {
                if (word.Length > max)
                    max = word.Length;
            }

            List<string> longest = words.Where(x => x.Length == max).ToList();

            return longest;
        }

        public List<string> GetShortestWords(string sentence)
        {
            //sentence = Regex.Replace(sentence, "[^a-zA-Z0-9% ._]", string.Empty);
            var punctuation = sentence.Where(Char.IsPunctuation).Distinct().ToArray();
            List<string> words = sentence.Split().Select(x => x.Trim(punctuation)).ToList();
            //string[] words = sentence.Split();

            int min = words[0].Length;
            foreach (var word in words)
            {
                if (word.Length < min)
                    min = word.Length;
            }

            List<string> shortest = words.Where(x => x.Length == min).ToList();

            return shortest;
        }
    }
}
