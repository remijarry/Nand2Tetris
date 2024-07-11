using System.Text;
using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public class LogicalCommand : ICommand
  {
    public string CommandType => "logical";
    public CommandName CommandName { get; set; }
    public int LineIndex { get; }


    public LogicalCommand(CommandName name, int lineIndex)
    {
      CommandName = name;
      LineIndex = lineIndex;
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
          // generate the unique return label
          sb.AppendLine($"@{Constants.Assembly.EQ_RETURN_LABEL_PREFIX}{LineIndex}");
          // store the return address in R5
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.EQ_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.EQ_RETURN_LABEL_PREFIX}{LineIndex})");
          // increment stack pointer
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.gt:
          sb.AppendLine($"@{Constants.Assembly.GT_RETURN_LABEL_PREFIX}{LineIndex}");
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.GT_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.GT_RETURN_LABEL_PREFIX}{LineIndex})");
          // increment stack pointer
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.lt:
          const string lessThanLabel = "LESS_THAN";
          const string notLessThanLabel = "NOT_LESS_THAN";
          const string endlessThanLabel = "END_LESS_THAN";
          // use jump instructions to select -1 or 0
          sb.Append(AsmCmds.SelectXAndYFromTheStack());
          sb.AppendLine(AsmCmds.SubDRegisterToRamValue());
          // align to x
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.ReferenceLabel(lessThanLabel));
          sb.AppendLine(AsmCmds.JumpLessThan("D"));
          sb.AppendLine(AsmCmds.ReferenceLabel(notLessThanLabel));
          sb.AppendLine(AsmCmds.JumpUnconditional());
          sb.AppendLine(AsmCmds.CreateLabel(lessThanLabel));
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetDToMinusOne());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.AppendLine(AsmCmds.ReferenceLabel(endlessThanLabel));
          sb.AppendLine(AsmCmds.JumpUnconditional());
          sb.AppendLine(AsmCmds.CreateLabel(notLessThanLabel));
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetDRegistertoRamValue());
          sb.AppendLine(AsmCmds.SetDToZero());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.AppendLine(AsmCmds.CreateLabel(endlessThanLabel));
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.or:
          sb.Append(AsmCmds.SelectXAndYFromTheStack());
          sb.AppendLine(AsmCmds.Or("D", "D", "M"));
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.and:
          sb.Append(AsmCmds.SelectXAndYFromTheStack());
          sb.AppendLine(AsmCmds.And("D", "D", "M"));
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.not:
          // align to y
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetDRegistertoRamValue());
          sb.AppendLine(AsmCmds.Not("D", "D"));
          sb.AppendLine(AsmCmds.AddOneToD());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        default:
          return string.Empty;

      }
    }
  }

}