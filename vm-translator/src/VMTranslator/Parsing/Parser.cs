using System;
using System.Collections.Generic;
using System.IO;
using VMTranslator.Commands;
using VMTranslator.Commands.Arithmetic;
using VMTranslator.Commands.Control;
using VMTranslator.Commands.Function;
using VMTranslator.Commands.Logical;
using VMTranslator.Commands.Memory;
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
          switch (memorySegment)
          {
            case MemorySegment.CONSTANT:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var constant))
              {
                list.Add(new Constant(constant, index));
              }
              break;
            case MemorySegment.TEMP:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var temp))
              {
                list.Add(new Temp(temp, index));
              }
              break;
            case MemorySegment.POINTER:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var pointer))
              {
                list.Add(new Pointer(pointer, index));
              }
              break;
            case MemorySegment.ARGUMENT:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var argument))
              {
                list.Add(new Argument(argument, index));
              }
              break;
            case MemorySegment.LOCAL:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var local))
              {
                list.Add(new Local(local, index));
              }
              break;
            case MemorySegment.STATIC:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var statc))
              {
                list.Add(new Static(statc, index));
              }
              break;
            case MemorySegment.THIS:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var dis))
              {
                list.Add(new This(dis, index));
              }
              break;
            case MemorySegment.THAT:
              if (Enum.TryParse<StackOperation>(typeStr.ToUpper(), out var dat))
              {
                list.Add(new That(dat, index));
              }
              break;
          }
          continue;
        }

        foreach (var command in _commandMap.Keys)
        {
          if (line.StartsWith(command))
          {
            list.Add(new Label(command.ToUpper(), labelId++));
            if (!functionSeen.ContainsKey(command))
            {
              functionSeen.Add(command, _commandMap[command]());
            }
            break;
          }
        }
      }


      list.Add(new End());
      list.AddRange(functionSeen.Values);
      list.Add(new True());
      list.Add(new False());

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