using System.Text;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public abstract class MemoryCommand : ICommand
  {
    public StackOperation StackOperation { get; }
    public string Index { get; }

    public abstract string Segment { get; }
    public MemoryCommand(StackOperation stackOperation, string index)
    {
      StackOperation = stackOperation;
      Index = index;
    }

    public StringBuilder Execute(StringBuilder sb)
    {
      if (StackOperation == StackOperation.PUSH)
      {
        return WritePush(sb);
      }

      return WritePop(sb);
    }

    public virtual StringBuilder WritePush(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.SegmentBaseAddress[Segment]}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Index}");
      sb.AppendLine($"D=D+A");
      sb.AppendLine("@R13");
      sb.AppendLine("M=D");
      sb.AppendLine("@R13");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"A=M");
      sb.AppendLine("M=D");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine($"M=M+1");
      return sb;
    }
    public virtual StringBuilder WritePop(StringBuilder sb)
    {
      sb.AppendLine($"@{Pointers.SegmentBaseAddress[Segment]}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Index}");
      sb.AppendLine("D=D+A");
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