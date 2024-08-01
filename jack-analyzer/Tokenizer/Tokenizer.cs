using System.Collections.Generic;
using System.IO;
using JackAnalyzer.Files;

namespace JackAnalyzer.Tokenizer
{
  public class Tokenizer : ITokenizer
  {
    private readonly string tokenPattern =
                              @"(?<Keyword>class|function|var|method|field|static|int|char|boolean|void|true|false|null|this|let|do|if|else|while|return) |
                                (?<Symbol>\{|\}|\[|\]|\(|\)|\.|\,|\;|\+|\-|\*|\/|\&|\||\<|\>|\=|\~) |
                                (?<Identifier>[a-zA-Z_][a-zA-Z_0-9]*) |
                                (?<IntegerConstant>\b(0|[1-9][0-9]{0,4}|[1-2][0-9]{4}|3[0-1][0-9]{2}|32[0-6][0-9]|327[0-6][0-7])\b) |
                                (?<StringConstant>""[^""\r\n]*"")";


    public IEnumerable<IToken> Tokenize(IEnumerable<JackFile> files)
    {
      var tokens = new List<IToken>();
      foreach (var file in files)
      {
        using (StreamReader sr = new StreamReader(file.Path))
        {
          tokens.AddRange(Tokenize(sr));
        }
      }

      return tokens;
    }

    private IEnumerable<IToken> Tokenize(StreamReader sr)
    {
      var tokenList = new List<IToken>();
      string line;
      while ((line = sr.ReadLine()) != null)
      {
        if (IsComment(line))
        {
          continue;
        }

        line = NormalizeString(line);

        if (string.IsNullOrWhiteSpace(line))
        {
          continue;
        }



      }
      return tokenList;
    }
    private static bool IsComment(string line)
    {
      return line.StartsWith('/');
    }

    /// <summary>
    /// Removes the comments and extra spaces from the string
    /// </summary>
    /// <returns></returns>
    private static string NormalizeString(string str)
    {
      if (string.IsNullOrWhiteSpace(str))
        return string.Empty;

      int index = str.IndexOf("//");
      if (index == -1)
      {
        return str.Trim();
      }
      else
      {
        return str[..index].Trim();
      }
    }
  }
}