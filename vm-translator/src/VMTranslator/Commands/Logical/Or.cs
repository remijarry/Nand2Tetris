using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Logical
{
    public class Or : ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("(OR)");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("M=M-1");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("M=M-1");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M");
            sb.AppendLine("D=M");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("M=M+1");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M");
            sb.AppendLine("D=D|M");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine($"M=M-1");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine($"A=M");
            sb.AppendLine($"M=D");
            sb.AppendLine($"@{Pointers.R13}");
            sb.AppendLine("A=M");
            sb.AppendLine("D=M");
            sb.AppendLine("0;JMP");
            return sb;
        }
    }
}