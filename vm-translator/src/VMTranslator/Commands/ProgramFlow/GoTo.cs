using System.Text;

namespace VMTranslator.Commands.ProgramFlow
{
  public class GoTo : ICommand
  {
    private string _label { get; }
    public GoTo(string label)
    {
      _label = label;
    }
    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine($"@{_label}");
      sb.AppendLine("0;JMP");
      return sb;
    }
  }
}