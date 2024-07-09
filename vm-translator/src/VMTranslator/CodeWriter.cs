
using System;
using System.Text;
using VMTranslator.Commands;
using VMTranslator.Enums;
using VMTranslator.Segments;

namespace VMTranslator
{
  public class CodeWriter
  {
    private readonly string _fileName;
    private StringBuilder sb = new();
    private SegmentManager _segmentManager;


    public CodeWriter(string fileName)
    {
      _fileName = fileName;
      _segmentManager = new SegmentManager();

    }

    public string WriteCommand(ICommand command)
    {
      return command.GetAssemblyCode();
    }

    public string WritePopCommand(MemoryAccessCommand command)
    {
      StringBuilder sb = new();


      // decrement stack pointer
      sb.AppendLine("@0");
      sb.AppendLine("M=M+1");
      return sb.ToString();
    }

    public void SetFileName(string fileName)
    {
      // todo
    }
  }
}