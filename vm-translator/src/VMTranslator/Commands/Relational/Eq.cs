using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Equals
    /// </summary>
    public class Eq : RelationalCommand, ICommand
    {
        private int lineIndex { get; }
        public Eq()
        {
            lineIndex = _index++;
        }

        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("// eq");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("AM=M-1");
            sb.AppendLine("D=M");
            sb.AppendLine("A=A-1");
            sb.AppendLine("D=M-D");
            sb.AppendLine($"@EQ_TRUE{lineIndex}");
            sb.AppendLine("D;JEQ");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=0");
            sb.AppendLine($"@EQ_END{lineIndex}");
            sb.AppendLine("0;JMP");
            sb.AppendLine($"(EQ_TRUE{lineIndex})");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine("A=M-1");
            sb.AppendLine("M=-1");
            sb.AppendLine($"(EQ_END{lineIndex})");
            return sb;
        }
    }
}