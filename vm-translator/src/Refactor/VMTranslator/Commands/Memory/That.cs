using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public class That : MemoryCommand
  {
    public That(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}