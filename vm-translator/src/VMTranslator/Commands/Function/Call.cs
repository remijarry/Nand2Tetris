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
    private readonly int _returnAddress;
    private readonly GoTo _goto;
    private readonly int _nArgs;

    public Call(int returnAddress, GoTo goTo, int nArgs)
    {
      _returnAddress = returnAddress;
      _goto = goTo;
      _nArgs = nArgs;
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
      sb.AppendLine($"({_goto.Label}.{ConsecutiveNumberGenerator.GetNext()})");
      return sb;
    }

    private StringBuilder PushReturnAddress(StringBuilder sb)
    {
      var c = new Constant(StackOperation.PUSH, _returnAddress.ToString());
      c.Execute(sb);
      return sb;
    }

    private StringBuilder PushLCL(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.LOCAL}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M+1");
      return sb;
    }

    private StringBuilder PushARG(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.ARGUMENT}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M+1");
      return sb;
    }

    private StringBuilder PushTHIS(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.THIS}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M+1");

      return sb;
    }

    private StringBuilder PushTHAT(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.THAT}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M+1");
      return sb;
    }

    private StringBuilder RepositionARG(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{_nArgs}");
      sb.AppendLine("D=D-A");
      sb.AppendLine($"@5");
      sb.AppendLine("D=D-A");
      sb.AppendLine($"@{Pointers.ARGUMENT}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      return sb;
    }

    private StringBuilder RepositionLCL(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Pointers.LOCAL}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      return sb;
    }

  }
}