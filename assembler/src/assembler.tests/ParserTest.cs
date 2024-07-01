namespace assembler.tests;

public class ParserTest
{
    [Theory]
    [InlineData("Add.asm", "Add.asm")]
    [InlineData("AddL.asm", "Add.asm")]
    [InlineData("Max.asm", "Max.asm")]
    [InlineData("MaxL.asm", "Max.asm")]
    [InlineData("Pong.asm", "Pong.asm")]
    [InlineData("PongL.asm", "Pong.asm")]
    [InlineData("Rect.asm", "Rect.asm")]
    [InlineData("RectL.asm", "Rect.asm")]
    public void Parse(string input, string expected)
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