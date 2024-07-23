using System.Text;
using VMTranslator.Commands.Function;
using VMTranslator.Constants;

namespace VMTranslator.Commands
{
  public class Bootstrap : ICommand
  {
    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine("// bootstrap");
      sb.AppendLine("@256");
      sb.AppendLine("D=A");
      sb.AppendLine($"@{Pointers.STACK}");
      sb.AppendLine("M=D");

      return sb;
    }
  }
}