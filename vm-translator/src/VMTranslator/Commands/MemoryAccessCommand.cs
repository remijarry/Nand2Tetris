using System;
using System.Collections.Generic;
using System.Text;
using VMTranslator.Constants;
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
      {VirtualSegment.TEMP, BaseAddress.TEMP},
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
          return WritePushCommand();
        case CommandName.pop:
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          if (Segment.Equals(VirtualSegment.TEMP))
          {
            sb.AppendLine($"@{SegmentAddresses[Segment] + Index}");
            sb.AppendLine($"D=A");
            sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
            sb.AppendLine("D=D+M");
            sb.AppendLine("A=D-M");
            sb.AppendLine("D=D-A");
            sb.AppendLine(AsmCmds.SetMemoryValueFrom(DRegister));
          }
          else if (Segment.Equals(VirtualSegment.POINTER))
          {
            sb.AppendLine($"@{SegmentAddresses[VirtualSegment.CONSTANT]}");
            sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
            sb.AppendLine(AsmCmds.SetDRegistertoRamValue());
            // 0 = this
            // pop the value in memory address 3
            if (Index == 0)
            {
              sb.AppendLine($"@{SegmentAddresses[VirtualSegment.THIS]}");
            }
            // 1 = that
            // pop the value in memory address 3
            else
            {
              sb.AppendLine($"@{SegmentAddresses[VirtualSegment.THAT]}");
            }
            sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          }
          else if (Segment.Equals(VirtualSegment.STATIC))
          {
            sb.AppendLine($"@{SegmentAddresses[VirtualSegment.CONSTANT]}");
            sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
            sb.AppendLine(AsmCmds.SetDRegistertoRamValue());
            sb.AppendLine($"@{CodeWriter.StaticPointer}");
            sb.AppendLine($"M=D");
            CodeWriter.IndexToMemAddr.Add(Index, CodeWriter.StaticPointer);
            CodeWriter.StaticPointer++;
          }
          else
          {
            sb.AppendLine($"@{SegmentAddresses[Segment]}");
            // select segment and store in D register
            sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
            sb.AppendLine(AsmCmds.SetDToA());
            // pop segment i
            // addr = segment base address + i
            sb.AppendLine($"@{Index}");
            sb.AppendLine(AsmCmds.AddDRegisterToARegister());
            sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
            // swap A and D

            sb.AppendLine("D=D+M");
            sb.AppendLine("A=D-M");
            sb.AppendLine("D=D-A");
            sb.AppendLine(AsmCmds.SetMemoryValueFrom(DRegister));
          }
          return sb.ToString();
        default:
          throw new NotSupportedException();
      }
      throw new NotImplementedException();
    }
    private string WritePushCommand()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// push {Segment} {Index}");
      switch (Segment)
      {
        case VirtualSegment.CONSTANT:
          sb.AppendLine(AsmCmds.SetDRegisterToIndex(Index));
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetMemoryValueFrom("D"));
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          break;
        case VirtualSegment.TEMP:
          sb.AppendLine($"@{SegmentAddresses[Segment] + Index}");
          sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
          sb.AppendLine(AsmCmds.SetDToA());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetMemoryValueFrom(DRegister));
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          break;
        case VirtualSegment.LOCAL:
        case VirtualSegment.ARGUMENT:
        case VirtualSegment.THAT:
        case VirtualSegment.THIS:
          sb.AppendLine($"@{SegmentAddresses[Segment]}");
          // select segment and store in D register
          sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
          sb.AppendLine(AsmCmds.SetDToA());
          sb.AppendLine($"@{Index}");
          // D contains the memory address
          sb.AppendLine(AsmCmds.AddDRegisterToARegister());
          sb.AppendLine("@R13");
          sb.AppendLine("M=D");
          sb.AppendLine("@R13");
          sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
          sb.AppendLine(AsmCmds.SetDRegistertoRamValue());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetMemoryValueFrom(DRegister));
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          break;
        case VirtualSegment.POINTER:
          // 0 = this
          // 1 = that
          if (Index == 0)
          {
            sb.AppendLine($"@{SegmentAddresses[VirtualSegment.THIS]}");
          }
          else
          {
            sb.AppendLine($"@{SegmentAddresses[VirtualSegment.THAT]}");
          }
          sb.AppendLine(AsmCmds.SetARegisterToMemoryValue());
          sb.AppendLine(AsmCmds.SetDToA());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetMemoryValueFrom("D"));
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          break;
        case VirtualSegment.STATIC:
          // get memory address of the Index value
          var memAddr = CodeWriter.IndexToMemAddr[Index];
          sb.AppendLine($"@{memAddr}");
          sb.AppendLine("A=M");
          sb.AppendLine("D=A");
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetMemoryValueFrom("D"));
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          break;
      }



      return sb.ToString();
    }
  }
}

