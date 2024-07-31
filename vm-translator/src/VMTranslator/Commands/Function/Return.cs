using System.Text;
using VMTranslator.Commands.Memory;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Function
{
  public class Return : ICommand
  {
    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine("// return");
      CreateReturnAddress(sb);
      RepositionReturnValue(sb);
      RestoreStackPointer(sb);
      RestoreThat(sb);
      RestoreThis(sb);
      RestoreArg(sb);
      RestoreLocal(sb);
      GoToReturnAddress(sb);
      return sb;
    }

    private static void CreateReturnAddress(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.LOCAL}");
      sb.AppendLine("D=M");
      sb.AppendLine($"@5");
      sb.AppendLine("A=D-A");
      sb.AppendLine("D=M");
      sb.AppendLine("@R15");
      sb.AppendLine("M=D");
    }

    private static void RepositionReturnValue(StringBuilder sb)
    {
      var pop = new Argument(StackOperation.POP, "0");
      pop.Execute(sb);
    }

    private static void RestoreStackPointer(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.ARGUMENT}");
      sb.AppendLine($"D=M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"M=D+1");
    }

    private static void RestoreThis(StringBuilder sb)
    {
      Restore(sb, 2, Pointers.THIS);
    }

    private static void RestoreThat(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.LOCAL}");
      sb.AppendLine("A=M-1");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.THAT}");
      sb.AppendLine("M=D");
    }

    private static void RestoreArg(StringBuilder sb)
    {
      Restore(sb, 3, Pointers.ARGUMENT);
    }

    private static void RestoreLocal(StringBuilder sb)
    {
      Restore(sb, 4, Pointers.LOCAL);
    }

    private static void Restore(StringBuilder sb, int addressDistance, int pointerBaseAddress)
    {
      sb.AppendLine($"@{addressDistance}");
      sb.AppendLine($"D=A");
      sb.AppendLine($"@{Pointers.LOCAL}");
      sb.AppendLine($"A=M-D");
      sb.AppendLine($"D=M");
      sb.AppendLine($"@{pointerBaseAddress}");
      sb.AppendLine($"M=D");
    }

    private static void GoToReturnAddress(StringBuilder sb)
    {
      sb.AppendLine("@15");
      sb.AppendLine("A=M");
      sb.AppendLine("0;JMP");
    }
  }
}