using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RPNLib
{
    internal static class Token
    {
        private static readonly string[] _operators = { "^", "*", "/", "+", "-" };
        private static readonly string[] _functions = { "sin", "cos"};

        public static int Precedence(string token)
        {
            return token switch
            {
                "sin" => 4,
                "cos" => 4,
                "*" => 3,
                "/" => 3,
                "+" => 2,
                "-" => 2,
                _ => 0
            };
        }

        public static double Evaluate(double value1, double value2, string token)
        {
            return token switch
            {
                "+" => value1 + value2,
                "-" => value1 - value2,
                "*" => value1 * value2,
                "/" => value1 / value2,
                _ => 0
            };
        }

        public static double Evaluate(double value, string token)
        {
            return token switch
            {
                "sin" => Math.Sin(value),
                "cos" => Math.Cos(value),
                _ => 0
            };
        }

        public static bool IsOperator(string token)
        {
            return _operators.Contains(token);
        }

        public static bool IsFunction(string token)
        {
            return _functions.Contains(token);
        }

        public static bool IsNumber(string token)
        {
            return Regex.Match(token, $@"[0-9]*\{Config.DecimalSeparator}?[0-9]+").Success;
            //return double.TryParse(token, out _, );
        }

        public static bool IsLeftParenthesis(string token)
        {
            return token == "(";
        }

        public static bool IsRightParenthesis(string token)
        {
            return token == ")";
        }

        public static bool IsGreaterPrecedence(string token1, string token2)
        {
            return Precedence(token1) >= Precedence(token2);
        }
    }
}
