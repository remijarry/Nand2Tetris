namespace JackAnalyzer.SyntaxAnalyzer;

public static class KeywordTable
{
    private static readonly Dictionary<string, TokenType> _keywords = new()
    {
        {"boolean", TokenType.BOOLEAN},
        {"char", TokenType.CHAR},
        {"class", TokenType.CLASS},
        {"constructor", TokenType.CONSTRUCTOR},
        {"do", TokenType.DO},
        {"else", TokenType.ELSE},
        {"field", TokenType.FIELD},
        {"false", TokenType.FALSE},
        {"function", TokenType.FUNCTION},
        {"if", TokenType. IF},
        {"int", TokenType.INT},
        {"let", TokenType.LET},
        {"method", TokenType.METHOD},
        {"null", TokenType.NULL},
        {"return", TokenType.RETURN},
        {"static", TokenType.STATIC},
        {"this", TokenType.STATIC},
        {"true", TokenType.STATIC},
        {"var", TokenType.STATIC},
        {"void", TokenType.STATIC},
        {"while", TokenType.WHILE}
    };

    public static bool TryGetKeyword(string text, out TokenType tokenType) => _keywords.TryGetValue(text, out tokenType);

}