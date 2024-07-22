using System;
using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Arithmetic
{
    public class Neg : ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("// neg");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=-M");
            return sb;
        }
    }
}