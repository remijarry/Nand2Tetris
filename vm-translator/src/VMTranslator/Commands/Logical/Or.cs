using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Logical
{
    public class Or : ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("// or");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("@R13");
            sb.AppendLine("M=D");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("@R13");
            sb.AppendLine("A=M");
            sb.AppendLine("D=D|A");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=D");
            return sb;
        }
    }
}