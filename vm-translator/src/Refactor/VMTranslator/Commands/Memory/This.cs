using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
  public class This : MemoryCommand
  {
    public override string Segment => MemorySegment.THIS;
    public This(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}