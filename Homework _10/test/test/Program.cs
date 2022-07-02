using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary;
            List<string> text;
            try
            {
                // чи повинна бібліотека Перекладача вміти працювати з файлами?
                // чи потрібно бібліотеці Перекладача мати клас Reader?

                // відповідальність Translator - отримати текст, провести маніпуляцію і віддати користовацькому коду
                // текст повинен ВЖЕ прийти до членів цієї бібліотеки у якомусь очікуванному форматі
                // (string, колекція string, stream...)
                text = Reader.ReadText(@"../../../Text.txt");

                // "дістати" із файлу словник це вже, на мою думку, входить у відповідальність цієї бібліотеки
                dictionary = Reader.ReadDictionre(@"../../../Dictionary.txt");

                // клас Reader має метод який починається зі слова Write
                // це суперечить назві класу
                // чому читач щось записує?

                // чому Translator зберігає текст у приватному полі?
                // а не приймає його і віддає переклад?

                // чому цей текст - string а не List<string> ?
                Translator translator = new Translator();
                translator.AddDictionary(dictionary);
                foreach (string i in text)
                {
                    translator.AddText(i);
                }

                string changedText = translator.ChangeWords();
                Console.WriteLine(changedText);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("FileNotFoundException");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
        }
    }
}