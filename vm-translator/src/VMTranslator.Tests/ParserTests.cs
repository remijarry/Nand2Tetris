namespace VMTranslator.Tests;

public class ParserTests
{

    [Fact]
    public void Push_Constant_X()
    {
        var inputFileDirectory = "Files/Input/Constant";
        var inputPath = Path.Combine(inputFileDirectory, "stack-test.vm");

        using FileStream fs = new(inputPath, FileMode.Open, FileAccess.Read);
        using StreamReader sr = new(fs);

        var parser = new Parser(sr);
        var result = parser.Parse();
        var c = 2;

    }
}