using System;
using System.Collections.Generic;
using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class MemoryAccessCommand : ICommand
  {
    public string CommandType => "memory access";
    private const string DRegister = "D";
    public VirtualSegment Segment { get; private set; }
    public int Index { get; private set; }
    public int LineIndex { get; }
    public CommandName CommandName { get; set; }

    private readonly Dictionary<VirtualSegment, int> SegmentAddresses = new()
    {
      {VirtualSegment.CONSTANT, BaseAddress.SP},
      {VirtualSegment.LOCAL, BaseAddress.LCL},
      {VirtualSegment.ARGUMENT, BaseAddress.ARG},
      {VirtualSegment.THIS, BaseAddress.THIS},
      {VirtualSegment.THAT, BaseAddress.THAT},
    };

    public MemoryAccessCommand(string name, string segment, string index, int lineIndex)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentException(nameof(name));
      }

      if (string.IsNullOrWhiteSpace(segment))
      {
        throw new ArgumentException(nameof(segment));
      }

      if (string.IsNullOrWhiteSpace(index))
      {
        throw new ArgumentException(nameof(index));
      }
      CommandName = (CommandName)Enum.Parse(typeof(CommandName), name);
      Segment = (VirtualSegment)Enum.Parse(typeof(VirtualSegment), segment.ToUpper()); // using ToUpper() because 'this' is a reserved keyword.
      Index = int.Parse(index);
      LineIndex = lineIndex;
    }

    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// {CommandName} {Segment} {Index}".ToLower());
      switch (CommandName)
      {
        case CommandName.push:
          return WritePushCommand(Index);
        case CommandName.pop:
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          // select segment and store in D register
          sb.AppendLine($"@{SegmentAddresses[Segment]}");
          sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
          sb.AppendLine(AsmCmds.SetDToA());
          // pop segment i
          sb.AppendLine($"@{Index}");
          sb.AppendLine(AsmCmds.AddDRegisterToARegister());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          // swap A and D
          // D=D+A
          // A=D-A
          // D=D-A

          sb.AppendLine("D=D+M");
          sb.AppendLine("A=D-M");
          sb.AppendLine("D=D-A");
          sb.AppendLine(AsmCmds.SetMemoryValueFrom(DRegister));

          return sb.ToString();
        default:
          throw new NotSupportedException();
      }
      throw new NotImplementedException();
    }
    private string WritePushCommand(int index)
    {
      var sb = new StringBuilder();
      sb.AppendLine(AsmCmds.SetDRegisterToIndex(index));
      sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
      sb.AppendLine(AsmCmds.SetMemoryValueFrom("D"));
      sb.AppendLine(AsmCmds.IncrementStackPointer());
      return sb.ToString();
    }
  }
}

