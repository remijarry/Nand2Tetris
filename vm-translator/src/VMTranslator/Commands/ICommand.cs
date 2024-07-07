using VMTranslator.Enums;

namespace VMTranslator.Commands
{
  public interface ICommand
  {
    public VirtualSegment Segment { get; set; }
    public int Index { get; set; }
    public string Execute();
  }
}