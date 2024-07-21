using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.ProgramFlow
{
  public class IfGoTo : ICommand
  {
    private string _label { get; }
    public IfGoTo(string label)
    {
      _label = label;
    }

    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine($"{Pointers.STACK}");
      sb.AppendLine("AM=M-1");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{_label}");
      sb.AppendLine("D;JNE");
      return sb;
    }
  }
}