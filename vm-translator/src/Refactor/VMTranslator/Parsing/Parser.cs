using System;
using System.Collections.Generic;
using System.IO;
using VMTranslator.Commands;
using VMTranslator.Commands.Arithmetic;
using VMTranslator.Commands.Function;
using VMTranslator.Commands.Logical;
using VMTranslator.Commands.Memory;
using VMTranslator.Commands.Relational;
using VMTranslator.Constants;
using Type = VMTranslator.Commands.Stack.Type;

namespace VMTranslator.Parsing
{
  public class Parser
  {
    private readonly StreamReader _streamReader;

    public Parser(StreamReader streamReader)
    {
      _streamReader = streamReader;
    }

    public List<ICommand> Parse()
    {
      var functionSeen = new HashSet<string>();
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
              if (Enum.TryParse<Type>(typeStr.ToUpper(), out var type))
              {
                list.Add(new Constant(type, index));
              }
              else
              {
                throw new ArgumentOutOfRangeException(nameof(typeStr));
              }
              break;
          }
        }
        else
        {

        }

        if (line.StartsWith(CommandName.ADD))
        {
          list.Add(new Label(CommandName.ADD, labelId++));
          if (!functionSeen.Contains(CommandName.ADD))
          {
            functionSeen.Add(CommandName.ADD);
            list.Add(new Add());
          }
          continue;
        }

        if (line.StartsWith(CommandName.SUB))
        {
          list.Add(new Label(CommandName.SUB, labelId++));
          if (!functionSeen.Contains(CommandName.SUB))
          {
            functionSeen.Add(CommandName.SUB);
            list.Add(new Sub());
          }
          continue;
        }

        if (line.StartsWith(CommandName.NEG))
        {
          list.Add(new Label(CommandName.NEG, labelId++));
          if (!functionSeen.Contains(CommandName.NEG))
          {
            functionSeen.Add(CommandName.NEG);
            list.Add(new Neg());
          }
          continue;
        }

        if (line.StartsWith(CommandName.AND))
        {
          list.Add(new Label(CommandName.AND, labelId++));
          if (!functionSeen.Contains(CommandName.AND))
          {
            functionSeen.Add(CommandName.AND);
            list.Add(new And());
          }
          continue;
        }

        if (line.StartsWith(CommandName.NOT))
        {
          list.Add(new Label(CommandName.NOT, labelId++));
          if (!functionSeen.Contains(CommandName.NOT))
          {
            functionSeen.Add(CommandName.NOT);
            list.Add(new Not());
          }
          continue;
        }

        if (line.StartsWith(CommandName.OR))
        {
          list.Add(new Label(CommandName.OR, labelId++));
          if (!functionSeen.Contains(CommandName.OR))
          {
            functionSeen.Add(CommandName.OR);
            list.Add(new Or());
          }
          continue;
        }

        if (line.StartsWith(CommandName.EQ))
        {
          list.Add(new Label(CommandName.EQ, labelId++));
          if (!functionSeen.Contains(CommandName.EQ))
          {
            functionSeen.Add(CommandName.EQ);
            list.Add(new Eq());
          }
          continue;
        }

        if (line.StartsWith(CommandName.LT))
        {
          list.Add(new Label(CommandName.LT, labelId++));
          if (!functionSeen.Contains(CommandName.LT))
          {
            functionSeen.Add(CommandName.LT);
            list.Add(new Lt());
          }
          continue;
        }

        if (line.StartsWith(CommandName.GT))
        {
          list.Add(new Label(CommandName.GT, labelId++));
          if (!functionSeen.Contains(CommandName.GT))
          {
            functionSeen.Add(CommandName.GT);
            list.Add(new Gt());
          }
          continue;
        }

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