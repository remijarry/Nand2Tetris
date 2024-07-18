using System.Collections.Generic;
using System.Linq;
using System.Text;
using VMTranslator.Commands;

namespace VMTranslator.Translation
{
  public class Translator
  {
    private List<ICommand> Commands { get; }

    public static int StaticPointer = 16;

    public static Dictionary<string, string> StaticIndexToMemAddrMap = new Dictionary<string, string>();

    public Translator(List<ICommand> commands)
    {
      Commands = commands;
    }

    public string Translate()
    {
      if (!Commands.Any())
      {
        return string.Empty;
      }

      var sb = new StringBuilder();
      foreach (var command in Commands)
      {
        command.Execute(sb);
      }

      return sb.ToString().Trim();
    }
  }
}