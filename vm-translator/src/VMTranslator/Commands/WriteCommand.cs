using VMTranslator.Segments;

namespace VMTranslator.Commands
{
  public class WriteCommand
  {
    public MemoryAccessCommand Command { get; set; }

    public SegmentManager SegmentManager { get; set; }

    public WriteCommand()
    {
      SegmentManager = new SegmentManager();
    }

    public abstract string Execute();
  }
}