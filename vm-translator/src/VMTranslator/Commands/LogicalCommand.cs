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
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.EQ_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.EQ_RETURN_LABEL_PREFIX}{LineIndex})");
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.gt:
          sb.AppendLine($"@{Constants.Assembly.GT_RETURN_LABEL_PREFIX}{LineIndex}");
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.GT_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.GT_RETURN_LABEL_PREFIX}{LineIndex})");
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.lt:
          sb.AppendLine($"@{Constants.Assembly.LT_RETURN_LABEL_PREFIX}{LineIndex}");
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.LT_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.LT_RETURN_LABEL_PREFIX}{LineIndex})");
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.or:
          sb.AppendLine($"@{Constants.Assembly.OR_RETURN_LABEL_PREFIX}{LineIndex}");
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.OR_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.OR_RETURN_LABEL_PREFIX}{LineIndex})");
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.and:
          sb.AppendLine($"@{Constants.Assembly.AND_RETURN_LABEL_PREFIX}{LineIndex}");
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.AND_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.AND_RETURN_LABEL_PREFIX}{LineIndex})");
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        case CommandName.not:
          sb.AppendLine($"@{Constants.Assembly.NOT_RETURN_LABEL_PREFIX}{LineIndex}");
          sb.Append(AsmCmds.StoreReturnAddressToR5());
          sb.Append(AsmCmds.JumpToFunction(Constants.Assembly.NOT_FUNCTION_NAME));
          sb.AppendLine($"({Constants.Assembly.NOT_RETURN_LABEL_PREFIX}{LineIndex})");
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          return sb.ToString();
        default:
          return string.Empty;

      }
    }
  }

}