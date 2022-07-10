using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPNLib
{
    public static class Postfix
    {
        public static double Evaluate(string postfix)
        {
            var postfixQueue = new Queue<string>();
            var inputArray = postfix.SplitMath();

            foreach (var input in inputArray)
                postfixQueue.Enqueue(input);

            var resultStack = new Stack<double>();

            while (postfixQueue.Count != 0)
            {
                var currentToken = postfixQueue.Dequeue();

                if (Token.IsNumber(currentToken))
                {
                    resultStack.Push(double.Parse(currentToken,Config.FormatInfo));
                    continue;
                }

                if (Token.IsOperator(currentToken))
                {
                    var val2 = resultStack.Pop();
                    var val1 = resultStack.Pop();
                    var output = Token.Evaluate(val1, val2, currentToken);
                    resultStack.Push(output);
                    continue;
                }

                if (Token.IsFunction(currentToken))
                {
                    var value = resultStack.Pop();
                    var result = Token.Evaluate(value, currentToken);
                    resultStack.Push(result);
                }
            }

            return resultStack.Pop();
        }
    }
}
