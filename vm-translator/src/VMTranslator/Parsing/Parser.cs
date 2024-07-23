using System;
using System.Collections.Generic;
using System.IO;
using VMtranslator.Commands;
using VMTranslator.Commands;
using VMTranslator.Commands.Arithmetic;
using VMTranslator.Commands.Function;
using VMTranslator.Commands.Logical;
using VMTranslator.Commands.Memory;
using VMTranslator.Commands.ProgramFlow;
using VMTranslator.Commands.Relational;
using VMTranslator.Constants;
using StackOperation = VMTranslator.Commands.Stack.StackOperation;

namespace VMTranslator.Parsing
{
  public class Parser
  {
    private readonly StreamReader _streamReader;
    private readonly ICommandFactory _commandFactory;

    public Parser(StreamReader streamReader)
    {
      _streamReader = streamReader;
      _commandFactory = new CommandFactory();
    }

    public List<ICommand> Parse()
    {
      var list = new List<ICommand>()
      {
        {new Bootstrap()}
      };

      string line;
      while ((line = _streamReader.ReadLine()) != null)
      {

        if (IsComment(line))
        {
          continue;
        }

        line = NormalizeString(line);

        if (string.IsNullOrWhiteSpace(line))
        {
          continue;
        }

        var command = _commandFactory.CreateCommand(line);
        list.Add(command);

      }
      return list;
    }

    private static bool IsComment(string line)
    {
      return line.StartsWith('/');
    }

    /// <summary>
    /// Removes the comments and extra spaces from the string
    /// </summary>
    /// <returns></returns>
    private static string NormalizeString(string str)
    {
      if (string.IsNullOrWhiteSpace(str))
        return string.Empty;

      int index = str.IndexOf("//");
      if (index == -1)
      {
        return str.Trim();
      }
      else
      {
        return str[..index].Trim();
      }
    }
  }
}