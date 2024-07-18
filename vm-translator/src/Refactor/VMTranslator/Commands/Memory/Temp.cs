using System.Text;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class Temp : MemoryCommand
  {
    public override string Segment => MemorySegment.TEMP;
    public Temp(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }

    public override StringBuilder WritePush(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.TEMP + Index}");
      sb.AppendLine($"A=M");
      sb.AppendLine($"D=A");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"M=M+1");

      return sb;

    }

    public override StringBuilder WritePop(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.TEMP + Index}");
      sb.AppendLine($"D=A");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"A=M");
      sb.AppendLine("D=D+M");
      sb.AppendLine("A=D-M");
      sb.AppendLine("D=D-A");
      sb.AppendLine("M=D");

      return sb;
    }
  }
}