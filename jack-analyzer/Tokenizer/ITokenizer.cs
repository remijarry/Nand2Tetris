using System.Collections.Generic;
using JackAnalyzer.Files;

namespace JackAnalyzer.Tokenizer
{
  public interface ITokenizer
  {
    public IEnumerable<IToken> Tokenize(IEnumerable<JackFile> files);
  }
}