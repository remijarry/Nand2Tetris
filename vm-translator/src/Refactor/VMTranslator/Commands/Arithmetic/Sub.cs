using System;

namespace VMTranslator.Commands.Arithmetic
{
  public class Sub : ArithmeticCommand
  {
    protected override string DecrementPointer()
    {
      return $"@{Constants.StackPointer}" +
             $"{Environment.NewLine}" +
             "M=M-1" +
             $"{Environment.NewLine}" +
             $"@{Constants.StackPointer}" +
             $"{Environment.NewLine}" +
             "M=M-1";
    }

    protected override string GetFunctionName()
    {
      return "(SUB)";
    }

    protected override string GetOperand()
    {
      return "D=D-M";
    }

    protected override string SelectSecondValue()
    {
      return $"@0" +
              $"{Environment.NewLine}" +
              "M=M+1" +
              $"{Environment.NewLine}" +
              "@0" +
              $"{Environment.NewLine}" +
              "A=M";
    }

    protected override string PushResult()
    {
      return $"@0" +
              $"{Environment.NewLine}" +
              "M=M-1" +
              $"{Environment.NewLine}" +
              "@0" +
              $"{Environment.NewLine}" +
              "A=M" +
              $"{Environment.NewLine}" +
              "M=D";
    }
  }
}