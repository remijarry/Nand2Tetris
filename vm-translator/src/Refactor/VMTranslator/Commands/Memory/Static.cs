using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public class Static : MemoryCommand
  {
    public Static(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}