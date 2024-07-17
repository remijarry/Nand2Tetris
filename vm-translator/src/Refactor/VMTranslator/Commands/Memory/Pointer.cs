using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public class Pointer : MemoryCommand
  {
    public Pointer(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}