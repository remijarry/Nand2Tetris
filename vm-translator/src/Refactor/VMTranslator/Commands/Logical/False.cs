using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Logical
{
    public class False : ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("(FALSE)");
            sb.AppendLine("@0");
            sb.AppendLine("A=M");
            sb.AppendLine("D=0");
            sb.AppendLine("M=D");
            sb.AppendLine($"@{Pointers.R13}");
            sb.AppendLine("A=M");
            sb.AppendLine("D=M");
            sb.AppendLine("0;JMP");
            return sb;
        }
    }
}