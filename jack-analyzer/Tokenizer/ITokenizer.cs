using System.Collections.Generic;
using JackAnalyser.Tokenizer;
using JackAnalyzer.Files;

namespace JackAnalyzer.Tokenizer
{
  public interface ITokenizer
  {
    public IEnumerable<Token> Tokenize(IEnumerable<JackFile> files);
  }
}