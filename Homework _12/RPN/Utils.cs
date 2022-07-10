using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RPNLib
{
    internal static class Utils
    {
        public static IEnumerable<string> SplitMath(this string line)
        {
            List<string> result = new List<string>();

            foreach (var match in Regex.Matches(line, @$"([*+/\-)(])|([0-9]*\{Config.DecimalSeparator}?[0-9]+)|cos|sin"))
            {
                result.Add(match.ToString());
            }

            return result;
        }
    }
}
