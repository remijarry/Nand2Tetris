using System.Xml.Linq;

namespace JackAnalyzer.Files
{
  public class FileWriter : IWriter
  {
    private readonly XDocument _xDocument;
    public FileWriter(XDocument xdoc)
    {
      _xDocument = xdoc;
    }
    public void WriteFile()
    {
      throw new System.NotImplementedException();
    }
  }
}