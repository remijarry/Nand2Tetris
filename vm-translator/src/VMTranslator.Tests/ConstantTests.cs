namespace VMTranslator.Tests;

public class ConstantTests
{

    [Theory]
    [InlineData("simple-push.vm","")]
    public void Push_Constant_X(string input, string expected)
    {
        var inputFileDirectory = "Files/Input/Constant";
        var inputPath = Path.Combine(inputFileDirectory, input);

        using FileStream fs = new(inputPath, FileMode.Open, FileAccess.Read);
        using StreamReader streamReader = new(fs);
    }
}