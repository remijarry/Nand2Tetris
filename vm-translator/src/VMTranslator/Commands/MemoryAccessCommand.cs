using System;
using VMTranslator.Enums;
using Action = VMTranslator.Enums.Action;

namespace VMTranslator.Commands
{
  public class MemoryAccessCommand : ICommand
  {
    public string CommandType => "memory access";

    public Action Action { get; set; }
    public VirtualSegment Segment { get; private set; }
    public int Index { get; private set; }

    // push constant 2
    // pop local 3
    public MemoryAccessCommand(string action, string segment, string index)
    {
      if (string.IsNullOrWhiteSpace(action))
      {
        throw new ArgumentException(nameof(action));
      }

      if (string.IsNullOrWhiteSpace(segment))
      {
        throw new ArgumentException(nameof(segment));
      }

      if (string.IsNullOrWhiteSpace(index))
      {
        throw new ArgumentException(nameof(index));
      }
      Action = (Action)Enum.Parse(typeof(Action), action);
      Segment = (VirtualSegment)Enum.Parse(typeof(VirtualSegment), segment.ToUpper()); // using ToUpper() because 'this' is a reserved keyword.
      Index = int.Parse(index);
    }
  }
}

