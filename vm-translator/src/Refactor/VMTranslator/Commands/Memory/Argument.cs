using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public class Argument : MemoryCommand
  {
    public Argument(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}