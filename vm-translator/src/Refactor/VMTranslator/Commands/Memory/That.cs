using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class That : MemoryCommand
  {
    public override string Segment => MemorySegment.THAT;
    public That(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}