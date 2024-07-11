
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
    private bool WriteTrueFalseFunctions;
    public Parser(StreamReader streamReader)
    {
      _streamReader = streamReader;
    }

    public ParsedFile Parse()
    {
      string line;
      int lineIndex = 0;
      var parsedFile = new ParsedFile();
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

          var memoryCommand = new MemoryAccessCommand(action, segment, index, lineIndex++);
          parsedFile.Commands.AddCommand(memoryCommand);
          list.AddCommand(memoryCommand);
          continue;
        }
        if (Enum.TryParse<CommandName>(line, out var command))
        {

          parsedFile.AddFunction(command.ToString());
          if (command == CommandName.add || command == CommandName.sub || command == CommandName.neg)
          {
            parsedFile.Commands.AddCommand(new ArithmeticCommand(command, lineIndex)); // we want to jump back to the next instruction after the cmd
          }
          else
          {
            WriteTrueFalseFunctions = true;
            parsedFile.Commands.AddCommand(new LogicalCommand(command, lineIndex));
          }
        }

      }

      if (WriteTrueFalseFunctions)
      {
        parsedFile.AddFunction("true");
        parsedFile.AddFunction("false");
      }

      return parsedFile;
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
