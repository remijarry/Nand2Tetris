using System;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Arithmetic
{
  public class Add : ArithmeticCommand
  {
    protected override string DecrementPointer()
    {
      return $"@{Pointers.STACK}" +
             $"{Environment.NewLine}" +
             "M=M-1" +
             $"{Environment.NewLine}" +
             $"@{Pointers.STACK}" +
             $"{Environment.NewLine}" +
             "M=M-1";
    }

    protected override string GetFunctionName()
    {
      return "(ADD)";
    }

    protected override string GetOperand()
    {
      return "D=D+M";
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