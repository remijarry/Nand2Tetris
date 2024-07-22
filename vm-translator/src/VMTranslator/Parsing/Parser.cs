using System;
using System.Collections.Generic;
using System.IO;
using VMTranslator.Commands;
using VMTranslator.Commands.Arithmetic;
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

    private readonly Dictionary<string, Func<ICommand>> _commandMap = new Dictionary<string, Func<ICommand>>()
    {
        { CommandName.ADD, () => new Add() },
        { CommandName.SUB, () => new Sub() },
        { CommandName.NEG, () => new Neg() },
        { CommandName.AND, () => new And() },
        { CommandName.NOT, () => new Not() },
        { CommandName.OR, () => new Or() },
        { CommandName.EQ, () => new Eq() },
        { CommandName.LT, () => new Lt() },
        { CommandName.GT, () => new Gt() }
    };

    private readonly Dictionary<string, Func<string, string, ICommand>> _memorySegmentMap = new Dictionary<string, Func<string, string, ICommand>>()
        {
            { MemorySegment.CONSTANT, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation constant) ? new Constant(constant, index) : null },
            { MemorySegment.TEMP, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation temp) ? new Temp(temp, index) : null },
            { MemorySegment.POINTER, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation pointer) ? new Pointer(pointer, index) : null },
            { MemorySegment.ARGUMENT, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation argument) ? new Argument(argument, index) : null },
            { MemorySegment.LOCAL, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation local) ? new Local(local, index) : null },
            { MemorySegment.STATIC, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation statc) ? new Static(statc, index) : null },
            { MemorySegment.THIS, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation dis) ? new This(dis, index) : null },
            { MemorySegment.THAT, (typeStr, index) => Enum.TryParse(typeStr.ToUpper(), out StackOperation dat) ? new That(dat, index) : null }
        };

    // might be overkilled
    private Stack<string> _labelStack = new Stack<string>();

    public Parser(StreamReader streamReader)
    {
      _streamReader = streamReader;
    }

    public List<ICommand> Parse()
    {
      var functionSeen = new Dictionary<string, ICommand>();
      var list = new List<ICommand>();

      string line;
      // for uniqueness
      // ie: @RETURN_ADD_labelId
      int labelId = 0;
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

        if (line.StartsWith(CommandName.PUSH) || line.StartsWith(CommandName.POP))
        {
          var tokens = line.Split(' ');
          var typeStr = tokens[0];

          var memorySegment = tokens[1];
          var index = tokens[2];
          if (_memorySegmentMap.TryGetValue(memorySegment, out var createCommand))
          {
            var command = createCommand(typeStr, index);
            if (command != null)
            {
              list.Add(command);
            }
          }
          continue;
        }

        if (line.StartsWith(CommandName.LABEL))
        {
          var tokens = line.Split(' ');
          _labelStack.Push(tokens[1].ToUpper());
          list.Add(new CommandLabel(tokens[1].ToUpper(), 0));
          continue;
        }

        if (line.StartsWith(CommandName.IF_GO_TO))
        {
          list.Add(new IfGoTo(_labelStack.Pop()));
        }

        foreach (var command in _commandMap.Keys)
        {
          if (line.StartsWith(command))
          {
            //list.Add(new CommandLabel(command.ToUpper(), labelId++));
            functionSeen.Add(command, _commandMap[command]());
            break;
          }
        }
      }


      list.AddRange(functionSeen.Values);
      list.Add(new End());
      // list.Add(new True());
      // list.Add(new False());

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