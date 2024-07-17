using System.Text;
using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public abstract class MemoryCommand : ICommand
  {
    public StackOperation StackOperation { get; }
    public string Index { get; }

    public MemoryCommand(StackOperation stackOperation, string index)
    {
      StackOperation = stackOperation;
      Index = index;
    }

    public StringBuilder Execute(StringBuilder sb)
    {
      throw new System.NotImplementedException();
    }
  }
}