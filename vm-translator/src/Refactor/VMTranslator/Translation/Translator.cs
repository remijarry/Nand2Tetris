using System.Collections.Generic;
using System.Linq;
using System.Text;
using VMTranslator.Commands;

namespace VMTranslator.Translation
{
  public class Translator
  {
    private List<ICommand> Commands { get; }

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

      return sb.ToString();
    }
  }
}