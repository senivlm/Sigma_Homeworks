using System;
using System.Collections.Generic;
using System.Text;

namespace MenuLib.Exceptions
{
    public class NoIngredientPriceException : Exception
    {
        //public override string Message => "Ingredient price was not found.";
        public NoIngredientPriceException() : base()
        {

        }

        public NoIngredientPriceException(string message) : base(message)
        {

        }

        public NoIngredientPriceException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
