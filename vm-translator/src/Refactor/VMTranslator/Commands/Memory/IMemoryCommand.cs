using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
  public interface IMemoryCommand
  {
    public Type Type { get; }
    public string Index { get; }
  }
}