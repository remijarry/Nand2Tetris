using System.Text;

namespace VMTranslator.Commands.Arithmetic
{
  public abstract class ArithmeticCommand : ICommand
  {
    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine(GetFunctionName());
      sb.AppendLine(DecrementPointer());
      sb.AppendLine(SelectFirstValue());
      sb.AppendLine(SelectSecondValue());
      sb.AppendLine(GetOperand());
      sb.AppendLine(PushResult());
      sb.AppendLine($"@{Constants.R13}");
      sb.AppendLine("A=M");
      sb.AppendLine("D=M");
      sb.AppendLine("0;JMP");

      return sb;

    }

    protected abstract string GetOperand();
    protected abstract string GetFunctionName();
    protected abstract string DecrementPointer();
    protected abstract string SelectSecondValue();

    protected abstract string PushResult();

    private static string SelectFirstValue()
    {
      return $"@{Constants.StackPointer}" +
              "A=M" +
              "D=M";
    }


  }
}