using System;
using System.Collections.Generic;
using System.Text;

namespace TranslatorLib
{
    public class WordNotFoundException : Exception
    {
        public WordNotFoundException() : base()
        {
        }

        public WordNotFoundException(string message) : base(message)
        {
        }

        public WordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }

    public class DictionaryIsEmptyException : Exception
    {
        public DictionaryIsEmptyException() : base()
        {

        }

        public DictionaryIsEmptyException(string message) : base(message)
        {
        }

        public DictionaryIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }


}
