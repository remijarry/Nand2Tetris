namespace JackAnalyzer.Scan;

public class Token
{
    public TokenType Type { get; }
    public string Lexeme { get; } = string.Empty;
    public object Literal { get; }
    public int Line { get;}

    public Token(TokenType type, string lexeme, object literal, int line)
    {
        Type = type;
        Lexeme = lexeme;
        Literal = literal;
        Line = line;    
    }

    public override string ToString()
    {
        // xml generation here?
        // probably not where it belongs
        return "To be implemented";
    }

}