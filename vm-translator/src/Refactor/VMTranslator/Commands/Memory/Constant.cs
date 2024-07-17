using System.Text;
using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class Constant : ICommand, IMemoryCommand
  {
    /// <summary>
    /// Number to push on the stack. It will also be used to select the equivalent memory address.
    /// </summary>
    /// <value></value>
    public Type Type { get; }
    public string Index { get; }

    public Constant(Type segmentType, string index)
    {
      Type = segmentType;
      Index = index;
    }
    public StringBuilder Execute(StringBuilder sb)
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