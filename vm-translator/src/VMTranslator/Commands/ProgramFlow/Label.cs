using System.Text;

namespace VMTranslator.Commands.ProgramFlow
{
  /// <summary>
  /// This class is in charge of creating a return label for a the arithmetic and logical commands so we don't duplicate code in case we have multiple adds in a row.
  /// The index must be unique per function.
  /// </summary>
  public class Label : ICommand
  {
    private string _name { get; }

    public Label(string Name)
    {
      _name = Name;
    }

    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine($"({_name})");
      return sb;
    }
  }
}