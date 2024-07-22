using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Logical
{
    public class Not : ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            sb.AppendLine("// not");
            sb.AppendLine($"@{Pointers.STACK}");
            sb.AppendLine($"A=M-1");
            sb.AppendLine($"M=!M");
            return sb;
        }
    }
}