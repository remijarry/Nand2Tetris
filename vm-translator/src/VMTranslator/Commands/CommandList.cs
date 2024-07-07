using System.Collections.Generic;

namespace VMTranslator.Commands
{
  public class CommandList
  {
    private List<ICommand> _commands;

    public CommandList()
    {
      _commands = new List<ICommand>();
    }

    public void AddCommand(ICommand command)
    {
      _commands.Add(command);
    }

    public List<ICommand> GetCommands()
    {
      return _commands;
    }
  }
}