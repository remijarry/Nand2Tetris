using VMTranslator.Commands;

namespace VMtranslator.Commands
{
  public interface ICommandFactory
  {
    ICommand CreateCommand(string line);
  }
}