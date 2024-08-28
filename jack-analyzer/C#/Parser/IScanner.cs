using System.Collections.Generic;
using JackAnalyser.Parser;

namespace JackAnalyzer.Parser
{
  public interface IScanner
  {
    public List<Token> Scan();
  }
}