using System;
using System.Text;

namespace VMTranslator.Commands.Relational
{
  public abstract class RelationalCommand
  {
    public StringBuilder GetAsm(StringBuilder sb)
    {
      sb.AppendLine(GetFunctionName());

      sb.AppendLine($"@{Constants.StackPointer}");
      sb.AppendLine("M=M-1");
      sb.AppendLine($"@{Constants.StackPointer}");
      sb.AppendLine("M=M-1");
      sb.AppendLine($"@{Constants.StackPointer}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Constants.StackPointer}");
      sb.AppendLine("M=M+1");
      sb.AppendLine($"@{Constants.StackPointer}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=D-M");
      sb.AppendLine($"@{Constants.StackPointer}");
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