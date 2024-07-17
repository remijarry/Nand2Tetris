using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Function
{
  /// <summary>
  /// This class is in charge of creating a return label for a function.
  /// The index must be unique per function.
  /// </summary>
  public class Label : ICommand
  {
    public string FunctionName { get; }
    public int Index { get; }

    public Label(string functionName, int index)
    {
      FunctionName = functionName;
      Index = index;
    }


    public StringBuilder Execute(StringBuilder sb)
    {
      // We set R13 to the return memory address and we jump to the function.
      // The function will jump back to return label.
      sb.AppendLine($"@RETURN_{FunctionName.ToUpper()}_{Index}");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Pointers.R13}");
      sb.AppendLine($"M=D");
      sb.AppendLine($"@{FunctionName}");
      sb.AppendLine("D=A");
      sb.AppendLine("0;JMP");
      sb.AppendLine($"(RETURN_{FunctionName}_{Index})");
      return sb;

    }
  }
}