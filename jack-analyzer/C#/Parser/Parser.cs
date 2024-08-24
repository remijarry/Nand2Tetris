using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JackAnalizer.Parser;
using JackAnalyzer.Tokenizer;

namespace JackAnalyzer.Parser
{
  public class Parser : IParser
  {
    private readonly IEnumerable<Token> _tokens;
    private readonly string _rootName;
    private XElement _root;

    /// <summary>
    /// index of the currently processed token.
    /// </summary>
    private int _index;
    public Parser(IEnumerable<Token> tokens, string rootName)
    {
      _tokens = tokens;
      _rootName = rootName;
      _root = new XElement(_rootName);
    }
    public XDocument Parse()
    {
      Class();
      return new XDocument(_root);
    }

    private void Class()
    {
      if (Peek().Text == "class")
      {
        _root.Add(CreateNode());

        if (Peek().Text == "Main")
        {
          _root.Add(CreateNode());
        }

        if (Peek().Text == "{")
        {
          _root.Add(CreateNode());
        }

        while (Peek().Text == "static" || Peek().Text == "field")
        {
          _root.Add(ClassVarDec());
        }

        while (Peek().Text == "constructor" || Peek().Text == "function" || Peek().Text == "method" || Peek().Text == "void")
        {
          _root.Add(SubRoutine());
        }
      }
      else
      {
        // error
      }
    }

    private XElement ClassVarDec()
    {
      var node = new XElement("classVarDec", string.Empty);
      while (_tokens.ElementAt(_index).Text != ";")
      {
        node.Add(CreateNode());
      }
      node.Add(CreateNode());
      return node;
    }

    /// <summary>
    /// ('constructor' | 'function' | 'method')
    /// ('void' | type) subroutineName '(' parameterList ')'
    /// subroutineBody
    /// </summary>
    private XElement SubRoutine()
    {
      var node = new XElement("subroutineDec", string.Empty);
      node.Add(CreateNode()); // type
      node.Add(CreateNode()); // return type
      node.Add(CreateNode()); // name
      node.Add(CreateNode()); // (
      node.Add(ParameterList());
      node.Add(CreateNode()); // )
      node.Add(SubroutineBody());
      return node;

    }

    /// <summary>
    /// ((type varName) (',' type varName)*)?
    /// </summary>
    private XElement ParameterList()
    {
      var node = new XElement("parameterList", string.Empty);
      while (_tokens.ElementAt(_index).Text != ")")
      {
        node.Add(CreateNode());
      }

      return node;
    }

    private XElement SubroutineBody()
    {
      var node = new XElement("subroutineBody", string.Empty);
      node.Add(VarDec());
      node.Add(Statements());
      return node;
    }

    /// <summary>
    /// 'var' type varName (',' varName)* ';'
    /// </summary>
    private XElement VarDec()
    {
      var node = new XElement("varDec", string.Empty);
      while (_tokens.ElementAt(_index).Text != ";")
      {
        node.Add(CreateNode());
      }
      node.Add(CreateNode());
      return node;
    }

    /// <summary>
    /// statements*
    /// </summary>
    private XElement Statements()
    {
      var node = new XElement("statements", string.Empty);
      while (Peek().Text != "}")
      {
        switch (Peek().Text)
        {
          case "let":
            node.Add(Let());
            break;
          case "if":
            node.Add(If());
            break;
          case "while":
            node.Add(While());
            break;
          case "do":
            node.Add(Do());
            break;
          case "return":
            node.Add(Return());
            break;
        }
      }

      return node;
    }

    private XElement Let()
    {
      var node = new XElement("letStatement", string.Empty);
      node.Add(CreateNode()); // let
      node.Add(CreateNode()); // varName
      if (Peek().Text == "[")
      {
        node.Add(CreateNode()); // [
        node.Add(Expression());
        node.Add(CreateNode()); // ]
        node.Add(CreateNode()); // '='
        node.Add(Expression());
        node.Add(CreateNode()); // ;
      }


      return node;
    }

    private XElement If()
    {
      var node = new XElement("ifStatement", string.Empty);

      return node;
    }

    private XElement While()
    {
      var node = new XElement("whileStatement", string.Empty);

      return node;
    }

    private XElement Do()
    {
      var node = new XElement("doStatement", string.Empty);

      return node;
    }

    private XElement Return()
    {
      var node = new XElement("returnStatement", string.Empty);

      return node;
    }


    /// <summary>
    /// term (op term)*
    /// </summary>
    /// <returns></returns>
    private XElement Expression()
    {
      return null;
    }

    /// <summary>
    /// integerConstant | stringConstant | keywordConstant |
    /// varName | varName '[' expression ']' | subroutineCall |
    // '(' expression ')' | unaryOp term
    /// </summary>
    /// <returns></returns>
    private XElement Term()
    {
      var node = new XElement("term", string.Empty);
      switch (Peek().Type)
      {
        case TokenType.integerConstant:
          break;
        case TokenType.stringConstant:
          break;
        case TokenType.identifier:
          if (_index + 1 < _tokens.Count() && PeekAt(_index + 1).Text == "[")
          {
            node.Add(CreateNode()); // [
          }
            break;

      }

      switch (Peek().Text)
      {
        case "true":
        case "false":
        case "null":
        case "this":
          break;

      }
      return node;
    }

    private XElement ExpressionList()
    {
      return null;
    }

    private Token Expect(TokenType type, params string[] values)
    {
      if (Peek().Type != type || !values.Contains(Peek().Text))
      {
        throw new Exception($"Expected one of {string.Join(", ", values)} but found {Peek().Text}");
      }

      var token = Peek();
      _index++;

      return token;
    }

    private Token Peek()
    {
      return _tokens.ElementAt(_index);
    }

    private Token PeekAt(int index)
    {
      if (index > _tokens.Count())
      {
        throw new IndexOutOfRangeException();
      }

      return _tokens.ElementAt(index);
    }

    private XElement CreateNode()
    {
      var el = new XElement(_tokens.ElementAt(_index).Type.ToString(), _tokens.ElementAt(_index).Text);
      _index++;
      return el;
    }

  }
}