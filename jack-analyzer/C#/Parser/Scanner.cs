using System.Collections.Generic;
using System.Linq;
using jack_analyzer;
using JackAnalyser.Parser;
using static JackAnalyzer.Parser.TokenType;

namespace JackAnalyzer.Parser
{
  public class Scanner : IScanner
  {
    private readonly string _source;
    private List<Token> _tokens = new();
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

    private Dictionary<string, TokenType> _keywords = new()
    {
      {"class", CLASS},
      {"constructor", CONSTUCTOR},
      {"method", METHOD},
      {"function", FUNCTION},
      {"int", INT},
      {"boolean", BOOLEAN},
      {"char", CHAR},
      {"void", VOID},
      {"var", VAR},
      {"static", STATIC},
      {"field", FIELD},
      {"let", LET},
      {"do", DO},
      {"if", IF},
      {"else", ELSE},
      {"while", WHILE},
      {"return", RETURN},
      {"true", TRUE},
      {"false", FALSE},
      {"null", NULL},
      {"this", THIS},
    };

    public Scanner(string source)
    {
      _source = source;
    }

    public List<Token> Scan()
    {
      while (!IsAtEnd())
      {
        _start = _current;
        ScanToken();
      }

      _tokens.Add(new Token(EOF, null));
      return _tokens;
    }

    private void ScanToken()
    {
      char c = Advance();
      switch (c)
      {
        case '(': AddToken(LEFT_PAREN); break;
        case ')': AddToken(RIGHT_PAREN); break;
        case '{': AddToken(LEFT_BRACE); break;
        case '}': AddToken(RIGHT_BRACE); break;
        case ',': AddToken(COMMA); break;
        case '.': AddToken(DOT); break;
        case '-': AddToken(MINUS); break;
        case '+': AddToken(PLUS); break;
        case ';': AddToken(SEMICOLON); break;
        case '*': AddToken(STAR); break;
        case '!':
          AddToken(Match('=') ? BANG_EQUAL : BANG);
          break;
        case '=':
          AddToken(Match('=') ? EQUAL_EQUAL : EQUAL);
          break;
        case '<':
          AddToken(Match('=') ? LESS_EQUAL : LESS);
          break;
        case '>':
          AddToken(Match('=') ? GREATER_EQUAL : GREATER);
          break;
        case '/':
          if (Match('/') || Match('*')) // comment to end of line
          {
            while (Peek() != '\n' && !IsAtEnd()) Advance();
          }
          else
          {
            AddToken(SLASH);
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
          String();
          break;
        default:
          if (IsDigit(c))
          {
            Number();
          }
          else
          if (IsAlpha(c))
          {
            Identifier();
          }
          else
          {
            Program.Error(_line, "Unexpected character");
          }
          break;
      }
    }

    private void String()
    {
      while (Peek() != '"' && !IsAtEnd())
      {
        if (Peek() == '\n')
        {
          _line++;
        }
        Advance();
      }

      if (IsAtEnd())
      {
        Program.Error(_line, "Unterminated string");
      }
      // closing double quote
      Advance();
      var value = _source.Substring(_start + 1, _current - 2 - _start);
      AddToken(STRING_CONSTANT, value);
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

      AddToken(INTEGER_CONSTANT, double.Parse(_source.Substring(_start, _current - _start)));
    }

    private void Identifier()
    {
      while (IsAlphaNumeric(Peek()))
      {
        Advance();
      }

      var word = _source.Substring(_start, _current - _start);
      var isKeyword = _keywords.TryGetValue(word, out var type);

      if (!isKeyword)
      {
        type = IDENTIFIER;
      }

      AddToken(type);
    }

    private bool IsDigit(char c)
    {
      return c >= '0' && c <= '9';
    }

    private bool IsAlpha(char c)
    {
      return
        (c >= 'a' && c <= 'z') ||
        (c >= 'A' && c <= 'Z') ||
        (c == '_');
    }

    private bool IsAlphaNumeric(char c)
    {
      return IsAlpha(c) || IsDigit(c);
    }

    private char Peek()
    {
      if (IsAtEnd())
      {
        return '\0';
      }

      return _source[_current];
    }

    private char PeekNext()
    {
      if (_current + 1 > _source.Length)
      {
        return '\0';
      }

      return _source[_current + 1];
    }

    private bool Match(char expected)
    {
      if (IsAtEnd())
      {
        return false;
      }

      if (_source[_current] != expected)
      {
        return false;
      }

      _current++;
      return true;
    }

    private void AddToken(TokenType type)
    {
      AddToken(type, null);
    }

    private void AddToken(TokenType type, object literal)
    {
      string text = _source.Substring(_start, _current - _start);
      _tokens.Add(new Token(type, text, literal, _line));
    }

    private bool IsAtEnd()
    {
      return _current >= _source.Length;
    }

    private char Advance()
    {
      _current++;
      return _source[_current - 1];
    }
  }
}