using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class ArithmeticCommand : ICommand
  {
    public string CommandType => "arithmetic";

    public CommandName Name { get; init; }

    public ArithmeticCommand(CommandName name)
    {
      Name = name;
    }

    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// {Name}");
      switch (Name)
      {
        case CommandName.add:
          // align to x
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SetDRegistertoRamValue());
          sb.AppendLine(CompCommands.IncrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.AddDRegisterToRamValue());
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SetRamValueToDRegister());
          sb.AppendLine(CompCommands.IncrementMemoryStackPointer());
          return sb.ToString();
        case CommandName.sub:
          // align to x
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.DecrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
          sb.AppendLine(CompCommands.SetDRegistertoRamValue());
          sb.AppendLine(CompCommands.IncrementMemoryStackPointer());
          sb.AppendLine(CompCommands.SelectStackPointerMemoryValue());
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