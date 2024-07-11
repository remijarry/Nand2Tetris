using System.Text;

namespace VMTranslator.Tests;

public class CodeWriterTests
{
  private string _inputFileDirectory;
  private string _expectedFileDirectory;

  public CodeWriterTests()
  {
    _inputFileDirectory = "Files/Input/Constant";
    _expectedFileDirectory = "Files/Expected/Constant";
  }


  [Theory]
  [InlineData("simple-push.vm", "simple-push.asm")]
  [InlineData("many-push.vm", "many-push.asm")]
  public void PushTwoConstants(string inputFile, string expectedFile)
  {
    var inputPath = Path.Combine(_inputFileDirectory, inputFile);

    using FileStream fs = new(inputPath, FileMode.Open, FileAccess.Read);
    using StreamReader sr = new(fs);

    var parser = new Parser(sr);
    var commands = parser.Parse();

    var codeWriter = new CodeWriter("test");
    var sb = new StringBuilder();
    foreach (var cmd in commands.Commands.GetCommands())
    {
      sb.AppendLine(codeWriter.WriteCommand(cmd));
    }

    var expectedPath = Path.Combine(_expectedFileDirectory, expectedFile);
    var expected = File.ReadAllText(expectedPath);

    Assert.Equal(expected, sb.ToString());
  }

  [Theory]
  [InlineData("simple-push-and-add.vm", "simple-push-and-add.asm")]
  // [InlineData("simple-push-and-sub.vm", "simple-push-and-sub.asm")]
  public void PushTwoConstantsAndDoArithmeticOperationOnThem(string inputFile, string expectedFile)
  {
    var inputPath = Path.Combine(_inputFileDirectory, inputFile);

    using FileStream fs = new(inputPath, FileMode.Open, FileAccess.Read);
    using StreamReader sr = new(fs);

    var parser = new Parser(sr);
    var commands = parser.Parse();

    var codeWriter = new CodeWriter("test");
    var sb = new StringBuilder();
    foreach (var cmd in commands.Commands.GetCommands())
    {
      sb.AppendLine(codeWriter.WriteCommand(cmd));
    }

    var expectedPath = Path.Combine(_expectedFileDirectory, expectedFile);
    var expected = File.ReadAllText(expectedPath);

    Assert.Equal(expected, sb.ToString());
  }

  // [Theory]
  // [InlineData("simple-push-and-eq.vm", "simple-push-and-eq.asm")]
  // [InlineData("simple-push-and-gt.vm", "simple-push-and-gt.asm")]
  // [InlineData("simple-push-and-lt.vm", "simple-push-and-lt.asm")]
  // [InlineData("simple-push-and-or.vm", "simple-push-and-or.asm")]
  // [InlineData("simple-push-and-and.vm", "simple-push-and-and.asm")]
  // [InlineData("simple-push-and-not.vm", "simple-push-and-not.asm")]
  // public void PushTwoConstantsAndDoLogicalOperationOnthem(string inputFile, string expectedFile)
  // {
  //   var inputPath = Path.Combine(_inputFileDirectory, inputFile);

  //   using FileStream fs = new(inputPath, FileMode.Open, FileAccess.Read);
  //   using StreamReader sr = new(fs);

  //   var parser = new Parser(sr);
  //   var commands = parser.Parse();

  //   var codeWriter = new CodeWriter("test");
  //   var sb = new StringBuilder();
  //   foreach (var cmd in commands.Commands.GetCommands())
  //   {
  //     sb.AppendLine(codeWriter.WriteCommand(cmd));
  //   }

  //   var expectedPath = Path.Combine(_expectedFileDirectory, expectedFile);
  //   var expected = File.ReadAllText(expectedPath);

  //   Assert.Equal(expected, sb.ToString());
  // }

}