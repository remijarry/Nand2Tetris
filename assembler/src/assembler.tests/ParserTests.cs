namespace assembler.tests;

using assembler;

public class ParseTests
{
    [Theory]
    [InlineData("Add.asm", "Add.asm")]
    [InlineData("MaxL.asm", "Max.asm")]
    [InlineData("PongL.asm", "Pong.asm")]
    [InlineData("RectL.asm", "Rect.asm")]
    public void ParseTest(string input, string expected)
    {
        var inputFileDirectory = "files/input";
        var inputPath = Path.Combine(inputFileDirectory, input);

        using FileStream fileStream = new(inputPath, FileMode.Open, FileAccess.Read);
        using StreamReader streamReader = new(fileStream);

        var parser = new Parser(streamReader);
        var result = parser.Parse();

        var expectedFileDirectory = "files/expected";
        var expectedPath = Path.Combine(expectedFileDirectory, expected);
        var expectedFile = File.ReadAllText(expectedPath);

        Assert.Equal(expectedFile, result);
    }
}