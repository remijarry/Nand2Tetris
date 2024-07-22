using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Greater than
    /// </summary>
    public class Gt : RelationalCommand, ICommand
    {
        private int lineIndex { get; }
        public Gt()
        {
            lineIndex = _index++;
        }
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("// gt");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("D=M-D");
            sb.AppendLine($"@GT_TRUE{lineIndex}");
            sb.AppendLine("D;JGT");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=0");
            sb.AppendLine($"@GT_END{lineIndex}");
            sb.AppendLine("0;JMP");
            sb.AppendLine($"(GT_TRUE{lineIndex})");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=-1");
            sb.AppendLine($"(GT_END{lineIndex})");
            return sb;
        }
    }
}