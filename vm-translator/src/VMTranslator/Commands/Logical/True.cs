using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Logical
{
    public class True : ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("(TRUE)");
            sb.AppendLine("@0");
            sb.AppendLine("A=M");
            sb.AppendLine("D=-1");
            sb.AppendLine("M=D");
            sb.AppendLine($"@{Pointers.R13}");
            sb.AppendLine("A=M");
            sb.AppendLine("D=M");
            sb.AppendLine("0;JMP");
            return sb;
        }
    }
}