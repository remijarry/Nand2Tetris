using JackAnalyzer.Tokenizer;

namespace JackAnalyser.Tokenizer
{
  public class Token
  {
    TokenType Type { get; set; }
    string Text { get; set; }

    public Token(TokenType type, string text)
    {
      Type = type;
      Text = text;
    }
  }
}