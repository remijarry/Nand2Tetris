using JackAnalyzer.Parser;

namespace JackAnalyser.Parser
{
  public class Token
  {
    public TokenType Type { get; }

    public string Lexeme { get; }

    public object Literal { get; set; }

    public int Line { get; set; }

    public Token(TokenType type, string lexeme)
    {
      Type = type;
      Lexeme = lexeme;
    }

    public Token(TokenType type, string lexeme, object literal, int line)
    {

    }
  }
}