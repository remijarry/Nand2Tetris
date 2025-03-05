using JackAnalyzer.Constants;
using JackAnalyzer.Text;
using System.Numerics;

namespace JackAnalyzer.SyntaxAnalyzer.Interfaces;

public class Tokenizer(string source) : ITokenizer
{
    private readonly SourceText _sourceText = new(source);
    private List<Token> _tokens = new();
    private int _line = 1;

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
        try
        {
            while (!_sourceText.IsAtEnd())
            {
                _sourceText.Begin();
                ScanToken();
            }

            _tokens.Add(new Token(TokenType.EOF, LexicalElement.EOF, "", null, _line));

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error at line {_line}");
            Console.WriteLine(e.Message);
        }
        return _tokens;
    }

    private void ScanToken()
    {
        char c = _sourceText.Advance();
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
            case '~': AddToken(TokenType.NOT, LexicalElement.SYMBOL); break;
            case '|': AddToken(TokenType.PIPE, LexicalElement.SYMBOL); break;
            case '&': AddToken(TokenType.ESPERLUETTE, LexicalElement.SYMBOL, "&amp;"); break;
            case '<':
                if (_sourceText.Match('>'))
                {
                    AddToken(TokenType.NOT_EQUAL, LexicalElement.SYMBOL);
                }
                else if (_sourceText.Match('='))
                {
                    AddToken(TokenType.LESS_EQUAL, LexicalElement.SYMBOL);
                }
                else
                {
                    AddToken(TokenType.LESS, LexicalElement.SYMBOL, "&lt;");
                }
                break;
            case '>':
                if (_sourceText.Match('='))
                {
                    AddToken(TokenType.GREATER_EQUAL, LexicalElement.SYMBOL);
                }
                else
                {
                    AddToken(TokenType.GREATER, LexicalElement.SYMBOL, "&gt;");
                }
                break;
            case '/':
                if (_sourceText.Peek() == '/')
                {
                    ScanSingleLineComment();
                }
                else if (_sourceText.Peek() == '*')
                {
                    ScanMultiLineComment();
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
                _line++;
                break;
            case '"':
                ScanString();
                break;
            default:
                if (IsDigit(c))
                {
                    ScanNumber();
                }
                else if (IsAlpha(c))
                {
                    ScanIdentifier();
                }
                else
                {
                    Program.Error(_line, "Unexpected character.");
                }
                break;
        }
    }

    private void ScanSingleLineComment()
    {
        if (_sourceText.Match('/'))
        {
            while (_sourceText.Peek() != '\n' && !_sourceText.IsAtEnd())
            {
                _sourceText.Advance();
            }

        }
    }

    private void ScanMultiLineComment()
    {
        if (_sourceText.Match('*'))
        {
            // Consume the initial '*'
            _sourceText.Advance();

            while (true)
            {
                if (_sourceText.IsAtEnd())
                {
                    // Handle the case where the comment is not closed properly.
                    // For example, you might want to throw an exception or simply break.
                    break;
                }

                if (_sourceText.Peek() == '*' && _sourceText.PeekAt(1) == '/')
                {
                    // Consume '*/'
                    _sourceText.Advance();  // Consume '*'
                    _sourceText.Advance();  // Consume '/'
                    break;
                }

                if (_sourceText.Peek() == '\n')
                {
                    _line++;
                }

                _sourceText.Advance();
            }
        }
    }

    private void ScanString()
    {
        // we don't want the quotes
        while (_sourceText.Peek() != '"' && !_sourceText.IsAtEnd())
        {
            if (_sourceText.Peek() == '\n')
            {
                _line++;
            }
            _sourceText.Advance();
        }

        if (_sourceText.IsAtEnd())
        {
            Program.Error(_line, "Unterminated string");
        }
        // closing "
        _sourceText.Advance();
        var str = _sourceText.GetSpecificWindow(1, 2);
        AddToken(TokenType.STRING, LexicalElement.STRING_CONSTANT, str.Replace("\"", string.Empty));
    }

    private void ScanNumber()
    {
        while (IsDigit(_sourceText.Peek()))
        {
            _sourceText.Advance();
        }

        if (_sourceText.Peek() == '.' && IsDigit(_sourceText.PeekAt(1)))
        {
            _sourceText.Advance();
            while (IsDigit(_sourceText.Peek()))
            {
                _sourceText.Advance();
            }
        }
        AddToken(TokenType.INT, LexicalElement.INTEGER_CONSTANT, _sourceText.GetCurrentWindow());
    }

    private void ScanIdentifier()
    {
        while (IsAlphaNumeric(_sourceText.Peek()))
        {
            _sourceText.Advance();
        }

        var text = _sourceText.GetCurrentWindow();
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

    private static bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
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

    private void AddToken(TokenType type, string lexicalElement)
    {
        AddToken(type, lexicalElement, null);
    }

    private void AddToken(TokenType type, string lexicalElement, object literal)
    {
        string text = _sourceText.GetCurrentWindow();
        _tokens.Add(new Token(type, lexicalElement, text, literal, _line));
    }

}

//TODO:
// Implement other kinds of comments. AT the moment, only // is handled.