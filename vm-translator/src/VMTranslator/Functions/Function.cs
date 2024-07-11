using System.Text;
using VMTranslator.Commands;

namespace VMTranslator.Functions
{
  /// <summary>
  /// Class in charge of writing the arithmetic functions once at the end of a file.
  /// </summary>
  public class Function : IFunction
  {
    public string FunctionName { get; }

    #region EQ
    private const string EQ_FUNCTION = "(EQ)";
    #endregion

    #region GT
    private const string GT_FUNCTION = "(GT)";
    #endregion
    private const string TRUE_LABEL = "TRUE";
    private const string FALSE_LABEL = "FALSE";

    public Function(string functionName)
    {
      FunctionName = functionName;
    }

    public string GetAssemblyCode()
    {
      var sb = new StringBuilder();
      switch (FunctionName)
      {
        // todo: magic string, use a constant or refactor enum CommandName
        case "eq":
          sb.AppendLine(EQ_FUNCTION);
          // decrement stack pointer by 2
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.Append(AsmCmds.SelectX());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          // select y
          sb.Append(AsmCmds.SelectY());
          // x = x - y
          sb.AppendLine(AsmCmds.SubDRegisterToRamValue());
          // decrement the pointer by one to store the result
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.JumpToTrueOrFalse("JEQ")); //todo: remove magic string
          return sb.ToString();
        case "gt":
          sb.AppendLine(GT_FUNCTION);
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.Append(AsmCmds.SelectX());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          // select y
          sb.Append(AsmCmds.SelectY());
          // if (x - y) > y jump to TRUE
          sb.AppendLine(AsmCmds.SubDRegisterToRamValue());
          // decrement the pointer by one to store the result
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.JumpToTrueOrFalse("JGT")); //todo: remove magic string
          return sb.ToString();
        case "true":
          return WriteTrueFunction();
        case "false":
          return WriteFalseFunction();
      }

      return "";
    }

    public string WriteTrueFunction()
    {
      var sb = new StringBuilder();
      sb.AppendLine($"({TRUE_LABEL})");
      // set return to -1
      sb.AppendLine("@0");
      sb.AppendLine("A=M");
      sb.AppendLine("D=-1");
      sb.AppendLine("M=D");
      // select the return address
      sb.Append(AsmCmds.SelectReturnAddressFromR5());
      return sb.ToString();
    }

    public string WriteFalseFunction()
    {
      var sb = new StringBuilder();
      // NOT EQUAL
      sb.AppendLine($"({FALSE_LABEL})");
      // set return to 0 (false)
      sb.AppendLine("@0");
      sb.AppendLine("A=M");
      sb.AppendLine("D=0");
      sb.AppendLine("M=D");
      // select the return address
      sb.Append(AsmCmds.SelectReturnAddressFromR5());
      return sb.ToString();
    }

  }
}