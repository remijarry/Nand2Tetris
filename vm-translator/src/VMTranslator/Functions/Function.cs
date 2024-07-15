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
    private const string EQ_FUNCTION = "(EQ)";
    private const string GT_FUNCTION = "(GT)";
    private const string LT_FUNCTION = "(LT)";
    private const string OR_FUNCTION = "(OR)";
    private const string AND_FUNCTION = "(AND)";
    private const string ADD_FUNCTION = "(ADD)";
    private const string NOT_FUNCTION = "(NOT)";
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
        case "lt":
          sb.AppendLine(LT_FUNCTION);
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.Append(AsmCmds.SelectX());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          sb.Append(AsmCmds.SelectY());
          // if (x - y) < y jump to TRUE
          sb.AppendLine(AsmCmds.SubDRegisterToRamValue());
          // decrement the pointer by one to store the result
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.JumpToTrueOrFalse("JLT")); //todo: remove magic string
          return sb.ToString();
        case "or":
          sb.AppendLine(OR_FUNCTION);
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.Append(AsmCmds.SelectX());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          sb.Append(AsmCmds.SelectY());
          sb.AppendLine(AsmCmds.Or("D", "D", "M"));
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          // sb.AppendLine(AsmCmds.IncrementStackPointer());
          sb.Append(AsmCmds.SelectReturnAddressFromR13());
          return sb.ToString();
        case "and":
          sb.AppendLine(AND_FUNCTION);
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.Append(AsmCmds.SelectX());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          sb.Append(AsmCmds.SelectY());
          sb.AppendLine(AsmCmds.And("D", "D", "M"));
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          // sb.AppendLine(AsmCmds.IncrementStackPointer());
          sb.Append(AsmCmds.SelectReturnAddressFromR13());
          return sb.ToString();
        case "not":
          sb.AppendLine(NOT_FUNCTION);
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetDRegistertoRamValue());
          sb.AppendLine(AsmCmds.Not("D", "D"));
          // sb.AppendLine(AsmCmds.AddOneToD()); TODO: test not on negative numbers
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          sb.Append(AsmCmds.SelectReturnAddressFromR13());
          return sb.ToString();
        case "add":
          sb.AppendLine(ADD_FUNCTION);
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.Append(AsmCmds.SelectX());
          sb.AppendLine(AsmCmds.IncrementStackPointer());
          sb.Append(AsmCmds.SelectY());
          sb.AppendLine(AsmCmds.AddDRegisterToRamValue());
          sb.AppendLine(AsmCmds.DecrementStackPointer());
          sb.AppendLine(AsmCmds.SelectStackPointerMemoryValue());
          sb.AppendLine(AsmCmds.SetRamValueToDRegister());
          // sb.AppendLine(AsmCmds.IncrementStackPointer());
          sb.Append(AsmCmds.SelectReturnAddressFromR13());
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
      sb.Append(AsmCmds.SelectReturnAddressFromR13());
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
      sb.Append(AsmCmds.SelectReturnAddressFromR13());
      return sb.ToString();
    }

  }
}