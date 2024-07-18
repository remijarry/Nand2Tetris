using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class Argument : MemoryCommand
  {
    public override string Segment => MemorySegment.ARGUMENT;

    public Argument(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }

  }
}