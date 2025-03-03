
using System.Collections;
using System.Text.RegularExpressions;
using System.Transactions;

namespace JackAnalyzer.Scan;

public class Tokenizer(string source) : ITokenizer
{
    private readonly string source = source;
    private List<Token> tokens = new();
    private int start = 0;
    private int current = 0;
    private int line = 1;

    private static readonly Dictionary<string, Type> keywords = new()
    {
        {"boolean", Type.BOOLEAN},
        {"char", Type.CHAR},
        {"class", Type.CLASS},
        {"constructor", Type.CONSTRUCTOR},
        {"do", Type.DO},
        {"else", Type.ELSE},
        {"field", Type.FIELD},
        {"false", Type.FALSE},
        {"function", Type.FUNCTION},
        {"if", Type. IF},
        {"int", Type.INT},
        {"let", Type.LET},
        {"method", Type.METHOD},
        {"null", Type.NULL},
        {"return", Type.RETURN},
        {"static", Type.STATIC},
        {"this", Type.STATIC},
        {"true", Type.STATIC},
        {"var", Type.STATIC},
        {"void", Type.STATIC},
        {"while", Type.WHILE}
    };


    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            start = current;
            ScanToken();
        }

        tokens.Add(new Token(new TokenType(Type.EOF, LexicalElement.EOF), "", null, line));
        return tokens;
    }

    private void ScanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': AddToken(Type.LEFT_PAREN, LexicalElement.SYMBOL); break;
            case ')': AddToken(Type.RIGHT_PAREN, LexicalElement.SYMBOL); break;
            case '{': AddToken(Type.LEFT_BRAC, LexicalElement.SYMBOL); break;
            case '}': AddToken(Type.LEFT_BRAC, LexicalElement.SYMBOL); break;
            case '[': AddToken(Type.RIGHT_SQUARE_BRAC, LexicalElement.SYMBOL); break;
            case ']': AddToken(Type.LEFT_SQARE_BRAC, LexicalElement.SYMBOL); break;
            case ',': AddToken(Type.COMMA, LexicalElement.SYMBOL); break;
            case '.': AddToken(Type.DOT, LexicalElement.SYMBOL); break;
            case '-': AddToken(Type.MINUS, LexicalElement.SYMBOL); break;
            case '+': AddToken(Type.PLUS, LexicalElement.SYMBOL); break;
            case ';': AddToken(Type.SEMI_COLUMN, LexicalElement.SYMBOL); break;
            case '*': AddToken(Type.STAR, LexicalElement.SYMBOL); break;
            case '=': AddToken(Type.EQUAL, LexicalElement.SYMBOL); break;
            case '~': AddToken(Type.TILDE, LexicalElement.SYMBOL); break;
            case '|': AddToken(Type.PIPE, LexicalElement.SYMBOL); break;
            case '&': AddToken(Type.ESPERLUETTE, LexicalElement.SYMBOL); break;
            case '<':
                if (Match('>'))
                {
                    AddToken(Type.NOT_EQUAL, LexicalElement.SYMBOL);
                }
                else if (Match('='))
                {
                    AddToken(Type.LESS_EQUAL, LexicalElement.SYMBOL);
                }
                else
                {
                    AddToken(Type.LESS, LexicalElement.SYMBOL);
                }
                break;
            case '>':
                AddToken(Match('=') ? Type.GREATER_EQUAL : Type.GREATER, LexicalElement.SYMBOL);
                break;
            case '/':
                if (Match('/'))
                {
                    while (Peek() != '\n' && !IsAtEnd())
                    {
                        Advance();
                    }
                }
                else
                {
                    AddToken(Type.SLASH, LexicalElement.SYMBOL);
                }
                break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                line++;
                break;
        }
    }

    public bool IsAtEnd()
    {
        return current >= source.Length;
    }

    private char Advance()
    {
        return source.ElementAt(current++);
        // or current++;
        // return source.ElementAt(current - 1)
    }

    private void AddToken(Type type, LexicalElement lexicalElement)
    {
        AddToken(type, lexicalElement, null);
    }

    private void AddToken(Type type, LexicalElement lexicalElement, object literal)
    {
        string text = source[start..current];
        tokens.Add(new Token(new TokenType(type, lexicalElement), text, literal, line));
    }

    private bool Match(char c)
    {
        if (IsAtEnd())
        {
            return false;
        }

        if (source.ElementAt(current) != c)
        {
            return false;
        }

        current++;
        return true;
    }

    private char Peek()
    {
        if (IsAtEnd())
        {
            return '\0';
        }

        return source.ElementAt(current);
    }

    private char PeekNext()
    {
        if (current + 1 > source.Length)
        {
            return '\0';
        }

        return source.ElementAt(current + 1);
    }
}