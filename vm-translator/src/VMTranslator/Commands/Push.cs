using System.Text;
using VMTranslator.Enums;
using VMTranslator.Segments;

namespace VMTranslator.Commands
{
  public class PushCommand : ICommand
  {
    public VirtualSegment Segment { get; set; }

    public int Index { get; set; }

    private SegmentManager _segmentManager = new();

    public PushCommand()
    {
    }

    public string Execute()
    {
      StringBuilder sb = new();
      sb.AppendLine($"// push {Segment} {Index}");

      sb.AppendLine($"@{Index}");
      sb.AppendLine("D=A");

      #region use the stack pointer to select ram address (RAM[RAM[0]])
      sb.AppendLine($"@{_segmentManager.GetStackPointerBaseAddress}");
      sb.AppendLine("A=M");
      #endregion

      // set memory address to index
      sb.AppendLine("M=D");
      // increment stack pointer
      sb.AppendLine("@0");
      sb.AppendLine("M=M+1");

      _segmentManager.IncrementPointer(Segment);

      return sb.ToString();
    }
  }
}