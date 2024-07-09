using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class ArithmeticCommand : ICommand
  {
    public string CommandType => "arithmetic";

    public CommandName CommandName { get; set; }

    public ArithmeticCommand(CommandName name)
    {
      CommandName = name;
    }

    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// {CommandName}");
      switch (CommandName)
      {
        case CommandName.add:
          // align to x
          sb.Append(CompCommands.SelectXAndYFromTheStack());
          sb.AppendLine(CompCommands.AddDRegisterToRamValue());
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SetRamValueToDRegister());
          sb.AppendLine(CompCommands.IncrementMemoryStackPointer());
          return sb.ToString();
        case CommandName.sub:
          // align to x
          sb.Append(CompCommands.SelectXAndYFromTheStack());
          sb.AppendLine(CompCommands.SubDRegisterToRamValue());
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SetRamValueToDRegister());
          sb.AppendLine(CompCommands.IncrementMemoryStackPointer());
          return sb.ToString();
        case CommandName.neg:
          // align to y
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SetDRegistertoRamValue());
          sb.AppendLine(CompCommands.NegateDRegisterValue());
          sb.AppendLine(CompCommands.SetRamValueToDRegister());
          sb.AppendLine(CompCommands.IncrementMemoryStackPointer());
          return sb.ToString();
        default:
          return string.Empty;
      }
    }
  }

}