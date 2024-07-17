using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public class This : MemoryCommand
  {
    public This(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}