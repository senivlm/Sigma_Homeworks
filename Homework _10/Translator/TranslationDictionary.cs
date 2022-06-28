using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TranslatorLib
{
    public class TranslationDictionary
    {
        private Dictionary<string, string> _dictionary;
        private string _filePath;

        public TranslationDictionary()
        {

        }
        public TranslationDictionary(string filePath)
        {
            _dictionary = TextProcessor.ConvertToDictionary(filePath);
            _filePath = filePath;
        }
        public string this[string key]
        {
            get
            {
                if(!_dictionary.ContainsKey(key))
                    throw new WordNotFoundException();
                
                return _dictionary[key];
            }
            
            set
            {
                if (!_dictionary.ContainsKey(key))
                    throw new WordNotFoundException();

                _dictionary[key] = value;
            }
        }

        public bool IsEmpty()
        {
            if (_dictionary == null)
                return true;
            if(_dictionary.Count == 0)
                return true;

            return false;
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(string key, string value)
        {
            _dictionary.Add(key, value);
        }

        public void AddToFile(string key, string value)
        {
            using (StreamWriter writer = File.AppendText(_filePath))
            {
                writer.Write("\n");
                writer.Write($"{key}-{value}");
            }
        }
    }
}
