using System.Text;

namespace VMTranslator.Commands
{
  public interface ICommand
  {
    // public string Execute(StringBuilder sb);
    public StringBuilder Execute(StringBuilder sb);
  }
}