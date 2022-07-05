using System;
using System.Collections.Generic;
using System.Text;

namespace RPNlib
{
    public static class RPN
    {
        public static double EvaluatePostfix(string postfix)
        {
            char[] separator = new char[] { ' ', '\t' };
            Stack<string> tks = new Stack<string>
                     (postfix.Split(separator, StringSplitOptions.RemoveEmptyEntries));

            double result = 0;
            try
            {
                result = EvalPostfix(tks);
                if (tks.Count != 0) throw new Exception();
            }
            catch (Exception e) { Console.WriteLine("error"); }
            return result;
        }

        private static double EvalPostfix(Stack<string> tks)
        {
            string tk = tks.Pop();
            double val1;
            double val2;
            if (!Double.TryParse(tk, out val1))
            {
                val2 = EvalPostfix(tks);
                val1 = EvalPostfix(tks);
                if (tk == "+") val1 += val2;
                else if (tk == "-") val1 -= val2;
                else if (tk == "*") val1 *= val2;
                else if (tk == "/") val1 /= val2;
                else throw new Exception();
            }
            return val1;
        }
    }
}
