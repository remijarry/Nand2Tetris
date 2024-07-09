using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public interface ICommand
  {
    public string CommandType { get; }

    public CommandName CommandName { get; set; }

    public string GetAssemblyCode();
  }
}