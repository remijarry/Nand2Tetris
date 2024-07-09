using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class LogicalCommand : ICommand
  {
    public string CommandType => "logical";

    public CommandName CommandName { get; set; }


    public LogicalCommand(CommandName name)
    {
      CommandName = name;
    }

    /// <summary>
    /// For equal (eq), greater than (gt) and less than (lt), -1 = true, 0 = false
    /// </summary>
    /// <returns></returns>
    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// {CommandName}");

      switch (CommandName)
      {
        case CommandName.eq:
          const string equalLabel = "EQUAL";
          const string notEqualLabel = "NOT_EQUAL";
          const string endLabel = "END_EQUAL";
          // use jump instructions to select -1 or 0
          sb.Append(CompCommands.SelectXAndYFromTheStack());
          sb.AppendLine(CompCommands.SubDRegisterToRamValue());
          // align to x
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.ReferenceLabel(equalLabel));
          sb.AppendLine(CompCommands.JumpEqual("D"));
          sb.AppendLine(CompCommands.ReferenceLabel(notEqualLabel));
          sb.AppendLine(CompCommands.JumpUnconditional());
          sb.AppendLine(CompCommands.CreateLabel(equalLabel));
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SubOneToD());
          sb.AppendLine(CompCommands.SetRamValueToDRegister());
          sb.AppendLine(CompCommands.ReferenceLabel(endLabel));
          sb.AppendLine(CompCommands.JumpUnconditional());
          sb.AppendLine(CompCommands.CreateLabel(notEqualLabel));
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SetDRegistertoRamValue());
          sb.AppendLine(CompCommands.SetDToZero());
          sb.AppendLine(CompCommands.SetRamValueToDRegister());
          sb.AppendLine(CompCommands.CreateLabel(endLabel));
          sb.AppendLine(CompCommands.IncrementMemoryStackPointer());
          return sb.ToString();
        default:
          return string.Empty;

      }
    }
  }

}