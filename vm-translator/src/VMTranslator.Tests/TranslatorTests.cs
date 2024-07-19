using System.Text;
using VMTranslator.Parsing;
using VMTranslator.Translation;

namespace VMTranslator.Tests;

public class TranslatorTests
{
  private string _inputFileDirectory;
  private string _expectedFileDirectory;

  public TranslatorTests()
  {
    _inputFileDirectory = "Files/Input";
    _expectedFileDirectory = "Files/Expected";
  }


  [Theory]
  [InlineData("many-push.vm", "many-push.asm")]
  [InlineData("stack-test.vm", "stack-test.asm")]
  [InlineData("basic-test.vm", "basic-test.asm")]
  [InlineData("pointer-test.vm", "pointer-test.asm")]
  public void OutputFileTest(string inputFile, string expectedFile)
  {
    var inputPath = Path.Combine(_inputFileDirectory, inputFile);

    using FileStream fs = new(inputPath, FileMode.Open, FileAccess.Read);
    using StreamReader sr = new(fs);

    var parser = new Parser(sr);
    var commands = parser.Parse();

    var translator = new Translator(commands);
    var result = translator.Translate();

    var expectedPath = Path.Combine(_expectedFileDirectory, expectedFile);
    var expected = File.ReadAllText(expectedPath);

    Assert.Equal(expected, result);
  }
}