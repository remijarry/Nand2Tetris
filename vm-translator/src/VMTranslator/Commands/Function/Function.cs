using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Function
{
  public class Function : ICommand
  {
    private readonly string _label;
    private readonly int _arguments;
    public Function(string label, int arguments)
    {
      _label = label;
      _arguments = arguments;
    }

    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine($"// function {_label} {_arguments}");
      sb.AppendLine($"({_label})");
      for (var i = 0; i < _arguments; i++)
      {
        sb.AppendLine($"@0");
        sb.AppendLine("D=A");
        sb.AppendLine($"@{Pointers.STACK}");
        sb.AppendLine("A=M");
        sb.AppendLine("M=D");
        sb.AppendLine($"@{Pointers.STACK}");
        sb.AppendLine("M=M+1");
      }
      return sb;
    }
  }
}