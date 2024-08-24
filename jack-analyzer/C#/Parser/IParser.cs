using System.Xml.Linq;

namespace JackAnalizer.Parser
{
  public interface IParser
  {
    public XDocument Parse();
  }
}