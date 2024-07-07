using System.IO;

namespace VMTranslator
{
  public class Parser
  {
    /// <summary>
    /// The file to process
    /// </summary>
    private readonly StreamReader _streamReader;
    public Parser(StreamReader streamReader)
    {
      _streamReader = streamReader;
    }
  }
}
