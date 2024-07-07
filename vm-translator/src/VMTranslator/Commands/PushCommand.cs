using System;
using System.Text;

namespace VMTranslator.Commands
{
  public class PushCommand : WriteCommand
  {
    // todo: delete
    public PushCommand() : base()
    {

    }


    public override string Execute()
    {
      StringBuilder sb = new();
      sb.AppendLine($"// {Command.Action} {Command.Segment} {Command.Index}");

      sb.AppendLine($"@{Command.Index}");
      sb.AppendLine("D=A");

      #region use the stack pointer to select ram address (RAM[RAM[0]])
      sb.AppendLine($"@{SegmentManager.GetStackPointerBaseAddress}");
      sb.AppendLine("A=M");
      #endregion

      // set memory address to index
      sb.AppendLine("M=D");
      // increment stack pointer
      sb.AppendLine("@0");
      sb.AppendLine("M=M+1");

      SegmentManager.IncrementPointer(Command.Segment);

      return sb.ToString();
    }
  }
}