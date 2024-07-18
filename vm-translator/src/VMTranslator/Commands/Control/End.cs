using System.Text;

namespace VMTranslator.Commands.Control
{
  public class End : ICommand
  {
    public StringBuilder Execute(StringBuilder sb)
    {
      sb.AppendLine("(END)");
      sb.AppendLine("@END");
      sb.AppendLine("0;JMP");

      return sb;
    }
  }
}
