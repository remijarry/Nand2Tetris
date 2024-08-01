using System;
using System.Text.RegularExpressions;

namespace jack_analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"(?<Keyword>class|function|var|method|field|static|int|char|boolean|void|true|false|null|this|let|do|if|else|while|return) |
                                (?<Symbol>\{|\}|\[|\]|\(|\)|\.|\,|\;|\+|\-|\*|\/|\&|\||\<|\>|\=|\~) |
                                (?<Identifier>[a-zA-Z_][a-zA-Z_0-9]*) |
                                (?<IntegerConstant>\b(0|[1-9][0-9]{0,4}|[1-2][0-9]{4}|3[0-1][0-9]{2}|32[0-6][0-9]|327[0-6][0-7])\b) |
                                (?<StringConstant>""[^""\r\n]*"")";
            Regex regex = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);

            string input = "class Main { var x = 10; let y = x + 1; if (y < 20) { return true; } else { return false; } }";

            MatchCollection matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                foreach (string groupName in regex.GetGroupNames())
                {
                    if (groupName == "0" || groupName == "1")
                        continue;
                    if (match.Groups[groupName].Success)
                    {
                        Console.WriteLine($"{groupName}: {match.Groups[groupName].Value}");
                    }
                }
            }
        }
    }
}
