namespace JackAnalyzer.SyntaxAnalyzer;

public class Token
{
    public TokenType Type { get; }
    public string LexicalElement { get; }
    public string Lexeme { get; } = string.Empty;
    public object Literal { get; }
    public int Line { get;}

    public Token(TokenType type, string lexicalElement, string lexeme, object literal, int line)
    {
        Type = type;
        LexicalElement = lexicalElement;
        Lexeme = lexeme;
        Literal = literal;
        Line = line;    
    }

    public override string ToString()
    {
        // xml generation here?
        // probably not where it belongs
        return $"<{LexicalElement}> {Lexeme} {Literal}</{LexicalElement}>";
    }

}

//TODO: 
// < should be &lt 
// > shoul be &gt
