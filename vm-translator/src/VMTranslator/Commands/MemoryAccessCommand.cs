using System;
using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class MemoryAccessCommand : ICommand
  {
    public string CommandType => "memory access";
    public VirtualSegment Segment { get; private set; }
    public int Index { get; private set; }
    public int LineIndex { get; }
    public CommandName CommandName { get; set; }


    // push constant 2
    // pop local 3
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
      switch (CommandName)
      {
        case CommandName.push:
          sb.AppendLine($"// {CommandName} {Segment} {Index}".ToLower());
          sb.AppendLine(AsmCmds.SetDRegisterToIndex(Index));
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetMemoryValueFrom("D"));
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.pop:
          break;
        default:
          throw new NotSupportedException();
      }
      throw new NotImplementedException();
    }
  }
}

