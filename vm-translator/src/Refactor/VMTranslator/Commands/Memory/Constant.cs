using System.Text;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class Constant : MemoryCommand
  {
    public override string Segment => MemorySegment.CONSTANT;

    public Constant(StackOperation segmentType, string index) : base(segmentType, index)
    {

    }

    public override StringBuilder WritePush(StringBuilder sb)
    {
      sb.AppendLine($"@{Index}");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M+1");
      return sb;
    }
  }
}