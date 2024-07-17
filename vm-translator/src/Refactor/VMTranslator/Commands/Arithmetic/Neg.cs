using System;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Arithmetic
{
    public class Neg : ArithmeticCommand
    {
        protected override string DecrementPointer()
        {
            return $"@{Pointers.STACK}" +
                    $"{Environment.NewLine}" +
                    "M=M-1";
        }

        protected override string GetFunctionName()
        {
            return "(NEG)";
        }

        protected override string GetOperand()
        {
            return "D=-D";
        }

        // no second value to select
        protected override string SelectSecondValue()
        {
            return string.Empty;
        }

        protected override string PushResult()
        {
            return "M=D";
        }
    }
}