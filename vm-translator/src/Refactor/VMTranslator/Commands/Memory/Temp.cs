using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public class Temp : MemoryCommand
  {
    public Temp(StackOperation stackOperation, string index) : base(stackOperation, index)
    {
    }
  }
}