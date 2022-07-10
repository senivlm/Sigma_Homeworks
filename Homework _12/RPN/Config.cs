using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RPNLib
{
    public static class Config
    {
        private static string _decimalSeparator = ".";
        internal static NumberFormatInfo FormatInfo = new NumberFormatInfo();

        public static string DecimalSeparator { 
            get 
            {
                return _decimalSeparator;
            } 
            set
            {
                _decimalSeparator = value;
                FormatInfo.NumberDecimalSeparator = value;
            }
        }
    }
}
