using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public interface ICommand
  {
    string CommandType { get; }

    CommandName Name { get; init; }

    public string GetAssemblyCode();
  }
}