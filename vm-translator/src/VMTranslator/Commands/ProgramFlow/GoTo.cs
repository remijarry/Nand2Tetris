using System.Text;
using VMTranslator.Addressing;

namespace VMTranslator.Commands.ProgramFlow
{
  public class GoTo : ICommand
  {
    public string Label { get; }
    public GoTo(string label)
    {
      Label = label;
    }
    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine($"// goto {Label}");
      sb.AppendLine($"@{Label}");
      sb.AppendLine("0;JMP");
      return sb;
    }
  }
}