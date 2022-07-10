using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPNLib
{
    public static class Infix
    {
        public static string GetPostfixString(string infix)
        {
            var queue = GetPostfixQueue(infix);
            var sb = new StringBuilder();

            while (queue.Count != 0)
                sb.Append(queue.Dequeue() + " ");

            return sb.ToString();
        }
        public static Queue<string> GetPostfixQueue(string infix)
        {
            var outputQueue = new Queue<string>();
            var operandStack = new Stack<string>();
            var inputArray = infix.SplitMath();

            foreach (var token in inputArray)
            {
                if (Token.IsNumber(token))
                {
                    outputQueue.Enqueue(token);
                    continue;
                }

                if (Token.IsLeftParenthesis(token) || Token.IsFunction(token))
                {
                    operandStack.Push(token);
                    continue;
                }

                if (Token.IsRightParenthesis(token))
                {
                    while (!Token.IsLeftParenthesis(operandStack.Peek()))
                    {
                        outputQueue.Enqueue(operandStack.Pop());
                    }

                    operandStack.Pop();
                    continue;
                }

                while (operandStack.Count != 0 && Token.IsGreaterPrecedence(operandStack.Peek(), token))
                {
                    outputQueue.Enqueue(operandStack.Pop());
                }

                operandStack.Push(token);
            }

            while (operandStack.Count > 0)
                outputQueue.Enqueue(operandStack.Pop());

            return outputQueue;
        }
    }
}
