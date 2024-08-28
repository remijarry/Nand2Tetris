using System.Collections.Generic;
using System.Linq;
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

    public void ScanToken()
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
          if (Match('/')) // comment to end of line
          {
            while (Peek() != '\n' && !IsAtEnd()) Advance();
          }
          else if (Match('*')) // comment until closing
          {
            while (Peek() != '/' && !IsAtEnd()) Advance();
          }
          else
          {
            AddToken(SLASH);
          }

          break;
      }
    }

    public char Peek()
    {
      if (IsAtEnd())
      {
        return '\0';
      }

      return _source[_current];
    }

    public char PeekNext()
    {
      if (_current + 1 > _source.Length)
      {
        return '\0';
      }

      return _source[_current + 1];
    }

    public bool Match(char expected)
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

    public void AddToken(TokenType type)
    {
      AddToken(type, null);
    }

    public void AddToken(TokenType type, object literal)
    {
      string text = _source.Substring(_start, _current - _start);
      _tokens.Add(new Token(type, text, literal, _line));
    }

    public bool IsAtEnd()
    {
      return _current >= _source.Length;
    }

    public char Advance()
    {
      _current++;
      return _source[_current - 1];
    }
  }
}