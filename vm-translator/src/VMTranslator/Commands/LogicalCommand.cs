using System;
using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class LogicalCommand : ICommand
  {
    public string CommandType => "logical";

    public CommandName Name { get; init; }

    public string GetAssemblyCode()
    {
      throw new NotImplementedException();
    }
  }

}