using System.Text;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class Static : MemoryCommand
  {
    public override string Segment => MemorySegment.STATIC;

    public int StaticPointer { get; set; }

    public int MemoryAddress { get; }

    public Static(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }

    public override StringBuilder WritePush(StringBuilder sb)
    {
      sb.AppendLine($"@{MemoryAddress}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"M=M+1");
      return sb;
    }

    public override StringBuilder WritePop(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.SegmentBaseAddress[MemorySegment.CONSTANT]}");
      sb.AppendLine($"A=M");
      sb.AppendLine($"D=M");
      sb.AppendLine($"@{StaticPointer}");
      sb.AppendLine($"M=D");
      return sb;
    }

  }
}