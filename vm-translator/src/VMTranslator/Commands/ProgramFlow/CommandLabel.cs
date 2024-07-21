using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.ProgramFlow
{
  /// <summary>
  /// This class is in charge of creating a return label for a the arithmetic and logical commands so we don't duplicate code in case we have multiple adds in a row.
  /// The index must be unique per function.
  /// </summary>
  public class CommandLabel : ICommand
  {
    public string FunctionName { get; }
    public int Index { get; }

    public CommandLabel(string functionName, int index)
    {
      FunctionName = functionName;
      Index = index;
    }

    public CommandLabel(string functionName)
    {
      FunctionName = functionName;
    }

    public StringBuilder Execute(StringBuilder sb)
    {
      // We set R13 to the return memory address and we jump to the function.
      // The function will jump back to return label.
      sb.AppendLine($"@RETURN_{FunctionName}_{Index}");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Pointers.R13}");
      sb.AppendLine($"M=D");
      sb.AppendLine($"@{FunctionName}");
      sb.AppendLine("D=A");
      sb.AppendLine("0;JMP");
      sb.AppendLine($"(RETURN_{FunctionName}_{Index})");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=M+1");

      return sb;
    }
  }
}