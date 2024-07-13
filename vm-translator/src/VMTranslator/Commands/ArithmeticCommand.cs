using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class ArithmeticCommand : ICommand
  {
    public string CommandType => "arithmetic";
    public CommandName CommandName { get; set; }
    public int LineIndex { get; }

    public ArithmeticCommand(CommandName name, int lineIndex)
    {
      CommandName = name;
    }

    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"// {CommandName}");
      switch (CommandName)
      {
        case CommandName.sub:
          // align to x
          sb.Append(AsmCmds.SelectXAndYFromTheStack());
          sb.AppendLine(AsmCmds.SubDRegisterToRamValue());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.neg:
          // align to y
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetDRegistertoRamValue());
          sb.AppendLine(AsmCmds.NegateDRegisterValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.add:
          sb.AppendLine($"@{Constants.Assembly.ADD_RETURN_LABEL_PREFIX}{LineIndex}");
          sb.Append(AsmCmds.StoreReturnAddressToR13());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.ADD_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.ADD_RETURN_LABEL_PREFIX}{LineIndex})");
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        default:
          return string.Empty;
      }
    }
  }

}