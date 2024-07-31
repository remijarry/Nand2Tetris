using System.Collections.Generic;

namespace VMTranslator.Files
{
  public class VMFiles
  {
    public List<string> Files { get; set; } = new List<string>();

    public bool IsDirectory { get; set; }
    public bool InvokeBootstrap => IsDirectory;
  }
}