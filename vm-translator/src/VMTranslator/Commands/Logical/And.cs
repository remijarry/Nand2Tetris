using System;
using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Logical
{
    public class And : ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("// and");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine($"@{Pointers.R13}");
            sb.AppendLine($"M=D");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine($"@{Pointers.R13}");
            sb.AppendLine("A=M");
            sb.AppendLine("D=D&A");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=D");
            return sb;
        }
    }
}