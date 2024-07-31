using System.IO;
namespace VMTranslator.Files
{
  public class FileWriter
  {
    public string AssemblyCode { get; }
    public string FilePath { get; }
    public FileWriter(string assemblyCode, string path)
    {
      AssemblyCode = assemblyCode;
      FilePath = path;
    }

    public void WriteFile(string path)
    {
      var inputDirectory = Path.GetDirectoryName(path);
      string outputFilePath = Path.Combine(inputDirectory, $"{Path.GetFileNameWithoutExtension(path)}.asm");
      File.WriteAllText(outputFilePath, AssemblyCode);
    }

    public void WriteDirectory(string path)
    {
      var inputDirectory = Path.GetDirectoryName(path);
      var directoryName = new DirectoryInfo(inputDirectory).Name;
      string outputFilePath = Path.Combine(inputDirectory, $"{directoryName}.asm");
      File.WriteAllText(outputFilePath, AssemblyCode);
    }
  }
}