
using System;
using System.IO;
using VMTranslator.Commands;
using VMTranslator.Enums;

namespace VMTranslator
{
  public class Parser
  {
    /// <summary>
    /// The file to process
    /// </summary>
    private readonly StreamReader _streamReader;
    public Parser(StreamReader streamReader)
    {
      _streamReader = streamReader;
    }

    public CommandList Parse()
    {
      string line;
      var list = new CommandList();
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

        if (line.StartsWith("push") || line.StartsWith("pop"))
        {
          var tokens = line.Split(' ');
          var action = tokens[0];
          var segment = tokens[1];
          var index = tokens[2];

          var memoryCommand = new MemoryAccessCommand(action, segment, index);
          list.AddCommand(memoryCommand);
          continue;
        }
        if (Enum.TryParse<CommandName>(line, out var command))
        {
          if (command == CommandName.add || command == CommandName.sub || command == CommandName.neg)
          {
            list.AddCommand(new ArithmeticCommand(command));
          }
          else
          {
            list.AddCommand(new LogicalCommand(command));
          }
        }

      }

      return list;
    }

    private bool IsComment(string line)
    {
      return line.StartsWith('/');
    }

    /// <summary>
    /// Removes the comments and extra spaces from the string
    /// </summary>
    /// <returns></returns>
    public string NormalizeString(string str)
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
