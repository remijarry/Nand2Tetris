using System.Collections.Generic;
using System.IO;
using VMtranslator.Commands;
using VMTranslator.Commands;
using VMTranslator.Files;

namespace VMTranslator.Parsing
{
  public class Parser
  {
    private readonly ICommandFactory _commandFactory;
    private readonly VMFiles input;

    public Parser(VMFiles Files)
    {
      _commandFactory = new CommandFactory();
      input = Files;
    }

    public List<ICommand> Parse()
    {
      var list = new List<ICommand>();
      if (input.InvokeBootstrap)
      {
        list.Add(new Bootstrap());
      }

      foreach (var file in input.Files)
      {
        using (StreamReader sr = new StreamReader(file))
        {
          string line;
          while ((line = sr.ReadLine()) != null)
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