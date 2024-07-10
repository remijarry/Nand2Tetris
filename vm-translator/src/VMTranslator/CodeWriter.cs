using System.Text;
using VMTranslator.Commands;

namespace VMTranslator
{
  public class CodeWriter
  {
    private readonly string _fileName;
    private StringBuilder sb = new();


    public CodeWriter(string fileName)
    {
      _fileName = fileName;
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