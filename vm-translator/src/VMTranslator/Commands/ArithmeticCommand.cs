using System;
using System.Text;

namespace VMTranslator.Commands
{
  public class ArithmeticCommand : ICommand
  {
    public string CommandType => "arithmetic";

    public ArithmeticCommandType Type { get; private set; }

    public ArithmeticCommand(ArithmeticCommandType type)
    {
      Type = type;
    }

    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// {Type}");
      switch (Type)
      {
        case ArithmeticCommandType.add:
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
        case ArithmeticCommandType.sub:
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
        case ArithmeticCommandType.neg:
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

  public enum ArithmeticCommandType
  {
    add,
    sub,
    neg,
    eq,
    gt,
    lt,
    and,
    or,
    not
  }

}