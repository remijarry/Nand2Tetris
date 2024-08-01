namespace JackAnalyzer.Tokenizer
{
  public interface IToken
  {
    string Text { get; set; }
    TokenType Type { get; set; }
  }
}