using System.Text;
using VMTranslator.Commands.Function;
using VMTranslator.Commands.ProgramFlow;
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
      var call = new Call(new GoTo("Sys.init"), 0);
      call.Execute(sb);
      return sb;
    }
  }
}