using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Files
{
  public abstract class Writer
  {
    protected readonly string _path;
    protected List<string> Files { get; set; }
    public Writer(string path)
    {
      if (!Path.IsPathRooted(path))
      {
        var currentDirectory = Directory.GetCurrentDirectory();
        _path = Path.Combine(currentDirectory, path);
      }
      else
      {
        _path = path;
      }
    }

    public virtual string GetPath()
    {
      return _path;
    }

    public abstract void WriteFile();
  }
}