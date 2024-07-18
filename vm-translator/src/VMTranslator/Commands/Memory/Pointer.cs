using System.Text;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class Pointer : MemoryCommand
  {
    public override string Segment => MemorySegment.POINTER;

    public Pointer(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }

    public override StringBuilder WritePush(StringBuilder sb)
    {
      if (Index == "0")
      {
        sb.AppendLine($"@{Pointers.THIS}");
      }
      else
      {
        sb.AppendLine($"@{Pointers.THAT}");
      }

      sb.AppendLine("A=M");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"M=M+1");

      return sb;
    }

    public override StringBuilder WritePop(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"M=M-1");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"A=M");
      sb.AppendLine($"D=M");

      if (Index == "0")
      {
        sb.AppendLine($"@{Pointers.THIS}");
      }
      else
      {
        sb.AppendLine($"@{Pointers.THAT}");
      }

      sb.AppendLine("M=D");

      return sb;
    }
  }
}