using System;
using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class LogicalCommand : ICommand
  {
    public string CommandType => "logical";

    public CommandName Name { get; init; }

    /// <summary>
    /// For equal (eq), greater than (gt) and less than (lt), -1 = true, 0 = false
    /// </summary>
    /// <returns></returns>
    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// {Name}");

      switch (Name)
      {
        case CommandName.eq:
          // use jump instructions to select -1 or 0
          return sb.ToString();
        default:
          return string.Empty;

      }
    }
  }

}