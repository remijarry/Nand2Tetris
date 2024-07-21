using System.Text;

namespace VMTranslator.Commands
{
  public interface ICommand
  {
    public StringBuilder Execute(StringBuilder sb);
  }
}