using System.Text;
using VMTranslator.Addressing;
using VMTranslator.Commands.Memory;
using VMTranslator.Commands.ProgramFlow;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Function
{
  public class Call : ICommand
  {
    private readonly GoTo _goto;
    private readonly int _nArgs;

    private readonly string _returnLabel;

    public Call(GoTo goTo, int nArgs)
    {
      _goto = goTo;
      _nArgs = nArgs;
      _returnLabel = $"RETURN.{_goto.Label}.{ConsecutiveNumberGenerator.GetNext()}";
    }

    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine($"// call {_goto.Label} {_nArgs}");
      PushReturnAddress(sb);
      PushLCL(sb);
      PushARG(sb);
      PushTHIS(sb);
      PushTHAT(sb);
      RepositionARG(sb);
      RepositionLCL(sb);
      _goto.Execute(sb);
      sb.AppendLine($"({_returnLabel})");
      return sb;
    }

    private StringBuilder PushReturnAddress(StringBuilder sb)
    {
      sb.AppendLine("// push return address");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("D=M");
      sb.AppendLine("M=M+1");
      sb.AppendLine($"@{Pointers.R13}");
      sb.AppendLine($"M=D");
      sb.AppendLine($"@{_returnLabel}");
      sb.AppendLine($"D=A");
      sb.AppendLine($"@{Pointers.R13}");
      sb.AppendLine($"A=M");
      sb.AppendLine($"M=D");
      return sb;
    }

    private StringBuilder PushLCL(StringBuilder sb)
    {
      sb.AppendLine("// push LCL");
      return MoveAddressToStack(sb, Pointers.LOCAL);
    }

    private StringBuilder PushARG(StringBuilder sb)
    {
      sb.AppendLine("// push ARG");

      return MoveAddressToStack(sb, Pointers.ARGUMENT);
    }

    private StringBuilder PushTHIS(StringBuilder sb)
    {
      sb.AppendLine("// push THIS");
      return MoveAddressToStack(sb, Pointers.THIS);
    }

    private StringBuilder PushTHAT(StringBuilder sb)
    {
      sb.AppendLine("// push THAT");
      return MoveAddressToStack(sb, Pointers.THAT);
    }

    private StringBuilder RepositionARG(StringBuilder sb)
    {
      sb.AppendLine("// reposition ARG");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.TEMP}");
      sb.AppendLine($"D=D-A");
      sb.AppendLine($"@{_nArgs}");
      sb.AppendLine($"D=D-A");
      sb.AppendLine($"@{Pointers.ARGUMENT}");
      sb.AppendLine($"M=D");

      return sb;
    }

    private static StringBuilder RepositionLCL(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.LOCAL}");
      sb.AppendLine($"M=D");
      return sb;
    }

    private StringBuilder MoveAddressToStack(StringBuilder sb, int segmentBasePointer)
    {

      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("D=M");
      sb.AppendLine("M=M+1");
      sb.AppendLine("@R13");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{segmentBasePointer}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=A");
      sb.AppendLine("@R13");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      return sb;
    }
  }
}
