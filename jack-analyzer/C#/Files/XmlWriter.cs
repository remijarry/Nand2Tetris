using System.Collections.Generic;
using System.Xml.Linq;
using JackAnalyzer.Tokenizer;

namespace JackAnalyzer.Files
{
  public class XmlWriter : Writer
  {
    private readonly List<Token> _tokens;
    private readonly string _rootName;

    public XmlWriter(string path, List<Token> tokens, string rootName) : base(path)
    {
      _tokens = tokens;
      _rootName = rootName;
    }
    public override void WriteFile()
    {
      var root = new XElement(_rootName);

      foreach (var token in _tokens)
      {
        // we want a space between the tags (ie: <el> string </el>)
        root.Add(new XElement(token.Type.ToString(), token.Text.PadLeft(token.Text.Length + 1, ' ').PadRight(token.Text.Length + 2, ' ')));
      }

      var xdoc = new XDocument(root);
    }
  }
}