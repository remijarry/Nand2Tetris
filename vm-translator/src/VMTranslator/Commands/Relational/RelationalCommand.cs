using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Relational
{
  public abstract class RelationalCommand : ICommand
  {
    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine(GetFunctionName());

      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M-1");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M-1");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M+1");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=D-M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M-1");
      sb.AppendLine("@TRUE");

      sb.AppendLine(GetJumpCondition());

      sb.AppendLine("@FALSE");
      sb.AppendLine("0;JMP");

      return sb;

    }

    protected abstract string GetFunctionName();

    protected abstract string GetJumpCondition();
  }
}