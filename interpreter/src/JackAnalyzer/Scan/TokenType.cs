namespace JackAnalyzer.Scan;

public class TokenType(Type type, LexicalElement LexicalElement)
{
    public Type Type { get; } = type;

    public LexicalElement LexicalElement { get; } = LexicalElement;
} 

public enum Type
{
    #region symbols
    LEFT_PAREN, RIGHT_PAREN, LEFT_BRAC, RIGHT_BRACK, LEFT_SQARE_BRAC, RIGHT_SQUARE_BRAC, 
    COMMA, DOT, SEMI_COLUMN, EQUAL, NOT_EQUAL, CLASS_MEMBERSHIP,
    PLUS, MINUS, STAR, SLASH, ESPERLUETTE, PIPE, TILDE,
    GREATER, LESS, GREATER_EQUAL, LESS_EQUAL,
    #endregion

    #region reserved words
    CLASS, CONSTRUCTOR, METHOD, FUNCTION, INT, BOOLEAN, CHAR, VOID, VAR, STATIC,
    FIELD, LET,DO, IF,ELSE, WHILE, RETURN, TRUE, FALSE, NULL, THIS,
    #endregion
    IDENTIFIER, INTEGER_CONSTANT,
    EOF
}

//TODO ~  this is the NOT operator