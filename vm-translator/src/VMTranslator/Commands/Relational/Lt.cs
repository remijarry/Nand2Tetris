using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Less than
    /// </summary>
    public class Lt : RelationalCommand, ICommand
    {
private int lineIndex { get; }
        public Lt()
        {
            lineIndex = _index++;
        }
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("// lt");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("D=M-D");
            sb.AppendLine($"@LT_TRUE{lineIndex}");
            sb.AppendLine("D;JLT");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=0");
            sb.AppendLine($"@LT_END{lineIndex}");
            sb.AppendLine("0;JMP");
            sb.AppendLine($"(LT_TRUE{lineIndex})");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=-1");
            sb.AppendLine($"(LT_END{lineIndex})");
            return sb;
        }
    }
}