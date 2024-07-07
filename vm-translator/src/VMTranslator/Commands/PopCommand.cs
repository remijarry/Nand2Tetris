using System.Text;

namespace VMTranslator.Commands
{
  public class PopCommand : WriteCommand
  {
    public PopCommand(MemoryAccessCommand command) : base(command)
    {
    }

    public override string Execute()
    {
      throw new System.NotImplementedException();
    }
  }
}