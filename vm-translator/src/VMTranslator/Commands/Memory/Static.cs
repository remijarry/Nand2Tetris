using System.Text;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;
using VMTranslator.Translation;

namespace VMTranslator.Commands.Memory
{
  public class Static : MemoryCommand
  {
    public override string Segment => MemorySegment.STATIC;

    public Static(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }

    public override StringBuilder WritePush(StringBuilder sb)
    {
      var memoryAddress = Translator.StaticIndexToMemAddrMap[Index];
      sb.AppendLine($"@{memoryAddress}");
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
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M-1");
      sb.AppendLine($"@{Pointers.SegmentBaseAddress[MemorySegment.CONSTANT]}");
      sb.AppendLine($"A=M");
      sb.AppendLine($"D=M");
      sb.AppendLine($"@{Translator.StaticPointer}");
      sb.AppendLine($"M=D");
      Translator.StaticIndexToMemAddrMap.Add(Index, Translator.StaticPointer.ToString());
      Translator.StaticPointer++;
      return sb;
    }

  }
}