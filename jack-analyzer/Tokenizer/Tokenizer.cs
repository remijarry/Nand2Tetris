using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using JackAnalyser.Tokenizer;
using JackAnalyzer.Files;

namespace JackAnalyzer.Tokenizer
{
  public class Tokenizer : ITokenizer
  {
    private readonly string tokenPattern =
                              @"(?<Keyword>class|constructor|function|method|field|static|var|int|char|boolean|void|true|false|null|this|let|do|if|else|while|return) |
                                (?<Symbol>\{|\}|\[|\]|\(|\)|\.|\,|\;|\+|\-|\*|\/|\&|\||\<|\>|\=|\~) |
                                (?<Identifier>[a-zA-Z_][a-zA-Z_0-9]*) |
                                (?<IntegerConstant>\b(0|[1-9][0-9]{0,4}|[1-2][0-9]{4}|3[0-1][0-9]{2}|32[0-6][0-9]|327[0-6][0-7])\b) |
                                (?<StringConstant>""[^""\r\n]*"")";


    public XDocument ToXml(IEnumerable<Token> tokens)
    {
      var root = new XElement("tokens");
      var xdoc = new XDocument(root);
      if (tokens == null || !tokens.Any())
      {
        return xdoc;
      }

      foreach (var token in tokens)
      {
        // we want a space between the tags (ie: <el> string </el>)
        root.Add(new XElement(token.Type.ToString(), token.Text.PadLeft(token.Text.Length + 1, ' ').PadRight(token.Text.Length + 2, ' ')));
      }

      return xdoc;

    }
    public IEnumerable<Token> Tokenize(IEnumerable<JackFile> files)
    {
      var tokens = new List<Token>();
      foreach (var file in files)
      {
        using (StreamReader sr = new StreamReader(file.Path))
        {
          tokens.AddRange(Tokenize(sr));
        }
      }

      return tokens;
    }

    private IEnumerable<Token> Tokenize(StreamReader sr)
    {
      var tokenList = new List<Token>();
      string line;
      var regex = new Regex(tokenPattern, RegexOptions.IgnorePatternWhitespace);
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

        MatchCollection matches = regex.Matches(line);

        foreach (Match match in matches)
        {
          foreach (string groupName in regex.GetGroupNames())
          {
            if (groupName == "0" || groupName == "1")
              continue;
            if (match.Groups[groupName].Success)
            {
              var tokenType = groupName;
              var camelCaseTokenType = ToCamelCase(tokenType);
              var value = match.Groups[groupName].Value;

              if (Enum.TryParse(camelCaseTokenType, out TokenType type))
              {
                if (type == TokenType.stringConstant)
                {
                  value = value.Replace("\"", string.Empty).Trim();
                }
                tokenList.Add(new Token(type, value));
              }
              continue;
            }
          }
        }

      }
      return tokenList;
    }

    private static string ToCamelCase(string tokenType)
    {
      string camelCaseTokenType = string.Empty;
      for (var i = 0; i < tokenType.Length; i++)
      {
        if (i == 0)
        {
          camelCaseTokenType += Char.ToLower(tokenType.ElementAt(i));
        }
        else
        {
          camelCaseTokenType += tokenType.ElementAt(i);
        }


      }

      return camelCaseTokenType;
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