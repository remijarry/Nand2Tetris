using JackAnalyzer.Constants;

namespace JackAnalyzer.SyntaxAnalyzer.Interfaces;

public class Tokenizer(string source) : ITokenizer
{
    private readonly string source = source;
    private List<Token> tokens = new();
    private int start = 0;
    private int current = 0;
    private int line = 1;

    private static readonly Dictionary<string, TokenType> keywords = new()
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


    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            start = current;
            ScanToken();
        }

        tokens.Add(new Token(TokenType.EOF, LexicalElement.EOF, "", null, line));
        return tokens;
    }

    private void ScanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': AddToken(TokenType.LEFT_PAREN, LexicalElement.SYMBOL); break;
            case ')': AddToken(TokenType.RIGHT_PAREN, LexicalElement.SYMBOL); break;
            case '{': AddToken(TokenType.LEFT_BRAC, LexicalElement.SYMBOL); break;
            case '}': AddToken(TokenType.LEFT_BRAC, LexicalElement.SYMBOL); break;
            case '[': AddToken(TokenType.RIGHT_SQUARE_BRAC, LexicalElement.SYMBOL); break;
            case ']': AddToken(TokenType.LEFT_SQARE_BRAC, LexicalElement.SYMBOL); break;
            case ',': AddToken(TokenType.COMMA, LexicalElement.SYMBOL); break;
            case '.': AddToken(TokenType.DOT, LexicalElement.SYMBOL); break;
            case '-': AddToken(TokenType.MINUS, LexicalElement.SYMBOL); break;
            case '+': AddToken(TokenType.PLUS, LexicalElement.SYMBOL); break;
            case ';': AddToken(TokenType.SEMI_COLUMN, LexicalElement.SYMBOL); break;
            case '*': AddToken(TokenType.STAR, LexicalElement.SYMBOL); break;
            case '=': AddToken(TokenType.EQUAL, LexicalElement.SYMBOL); break;
            case '~': AddToken(TokenType.TILDE, LexicalElement.SYMBOL); break;
            case '|': AddToken(TokenType.PIPE, LexicalElement.SYMBOL); break;
            case '&': AddToken(TokenType.ESPERLUETTE, LexicalElement.SYMBOL); break;
            case '<':
                if (Match('>'))
                {
                    AddToken(TokenType.NOT_EQUAL, LexicalElement.SYMBOL);
                }
                else if (Match('='))
                {
                    AddToken(TokenType.LESS_EQUAL, LexicalElement.SYMBOL);
                }
                else
                {
                    AddToken(TokenType.LESS, LexicalElement.SYMBOL, "&lt");
                }
                break;
            case '>':
                if (Match('='))
                {
                    AddToken(TokenType.GREATER_EQUAL, LexicalElement.SYMBOL);
                }
                else
                {
                    AddToken(TokenType.GREATER, LexicalElement.SYMBOL, "&gt");
                }
                break;
            case '/':
                if (Match('/'))
                {
                    while (Peek() != '\n' && !IsAtEnd())
                    {
                        Advance();
                    }
                }
                else if (Match('*'))
                {
                    if (Peek() == '*') 
                    {
                        Advance();
                    }

                    while (Peek() != '*' && PeekNext() != '/')
                    {
                        Advance();
                    }
                    // skip * and /
                    Advance();
                    Advance();
                }
                else
                {
                    AddToken(TokenType.SLASH, LexicalElement.SYMBOL);
                }
                break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                line++;
                break;
            case '"':
                String();
                break;
            default:
                if (IsDigit(c))
                {
                    Number();
                }
                else if (IsAlpha(c))
                {
                    Identifier();
                }
                else
                {
                    Program.Error(line, "Unexpected character.");
                }
                break;
        }
    }

    private bool IsAtEnd()
    {
        return current >= source.Length;
    }

    private char Advance()
    {
        return source.ElementAt(current++);
        // or current++;
        // return source.ElementAt(current - 1)
    }

    private void String()
    {
        while (Peek() != '"' && !IsAtEnd())
        {
            if (Peek() == '\n')
            {
                line++;
            }
            Advance();
        }

        if (IsAtEnd())
        {
            Program.Error(line, "Unterminated string");
        }
        // closing "
        Advance();
        var value = source.Substring(start + 1, current - 2 - start);
        AddToken(TokenType.STRING, LexicalElement.STRING_CONSTANT, value);
    }

    private static bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    private void Number()
    {
        while (IsDigit(Peek()))
        {
            Advance();
        }

        if (Peek() == '.' && IsDigit(PeekNext()))
        {
            Advance();
            while (IsDigit(Peek()))
            {
                Advance();
            }
        }
        AddToken(TokenType.INT, LexicalElement.INTEGER_CONSTANT, source.Substring(start, current - start));
    }

    private static bool IsAlpha(char c)
    {
        return
         (c >= 'a' && c <= 'z') ||
         (c >= 'A' && c <= 'Z') ||
         (c == '_');
    }

    private static bool IsAlphaNumeric(char c)
    {
        return IsAlpha(c) || IsDigit(c);
    }

    private void Identifier()
    {
        while (IsAlphaNumeric(Peek()))
        {
            Advance();
        }

        var text = source.Substring(start, current - start);
        var isKeyword = keywords.TryGetValue(text, out var keyword);
        if (!isKeyword)
        {
            keyword = TokenType.IDENTIFIER;
            AddToken(keyword, LexicalElement.IDENTIFIER);
        }
        else
        {
            AddToken(keyword, LexicalElement.KEYWORD);
        }

    }

    private void AddToken(TokenType type, string lexicalElement)
    {
        AddToken(type, lexicalElement, null);
    }

    private void AddToken(TokenType type, string lexicalElement, object literal)
    {
        string text = source[start..current];
        tokens.Add(new Token(type, lexicalElement, text, literal, line));
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

//TODO:
// Implement other kinds of comments. AT the moment, only // is handled.