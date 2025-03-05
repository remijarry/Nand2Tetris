namespace JackAnalyzer.Tests;

using System.IO;
using System.Text;
using JackAnalyzer.SyntaxAnalyzer.Interfaces;
using Xunit;

public class TokenizerTests
{
    [Theory]
    [InlineData("TestCases/ExpressionLessSquare/Main.jack")]
    [InlineData("TestCases/ExpressionLessSquare/Square.jack")]
    [InlineData("TestCases/ExpressionLessSquare/SquareGame.jack")]
    public void Tokenizer_ExpressionLess_ShouldGenerateCorrectTokens(string relativeFilePath)
    {
        string source = File.ReadAllText(relativeFilePath);

        ITokenizer tokenizer = new Tokenizer(source);

        var tokens = tokenizer.ScanTokens();

        var expectedOutputPath = relativeFilePath.Replace(".jack", "T.xml");
        string expectedOutput = File.ReadAllText(expectedOutputPath);

        var sb = new StringBuilder();
        sb.AppendLine("<tokens>\r");
        foreach (var token in tokens)
        {
            if (token.Type == SyntaxAnalyzer.TokenType.EOF)
            {
                break;
            }

            sb.Append(token.ToString());
        }
        sb.AppendLine("</tokens>\r");

        Assert.Equal(expectedOutput, sb.ToString());
    }


    [Theory]
    [InlineData("TestCases/ArrayTest/Main.jack")]
    public void Tokenizer_ArrayTest_ShouldGenerateCorrectTokens(string relativeFilePath)
    {
        string source = File.ReadAllText(relativeFilePath);

        ITokenizer tokenizer = new Tokenizer(source);

        var tokens = tokenizer.ScanTokens();

        var expectedOutputPath = relativeFilePath.Replace(".jack", "T.xml");
        string expectedOutput = File.ReadAllText(expectedOutputPath);

        var sb = new StringBuilder();
        sb.AppendLine("<tokens>\r");
        foreach (var token in tokens)
        {
            if (token.Type == SyntaxAnalyzer.TokenType.EOF)
            {
                break;
            }

            sb.Append(token.ToString());
        }
        sb.AppendLine("</tokens>\r");

        Assert.Equal(expectedOutput, sb.ToString());
    }

        [Theory]
    [InlineData("TestCases/Square/Main.jack")]
    [InlineData("TestCases/Square/Square.jack")]
    [InlineData("TestCases/Square/SquareGame.jack")]
    public void Tokenizer_Square_ShouldGenerateCorrectTokens(string relativeFilePath)
    {
        string source = File.ReadAllText(relativeFilePath);

        ITokenizer tokenizer = new Tokenizer(source);

        var tokens = tokenizer.ScanTokens();

        var expectedOutputPath = relativeFilePath.Replace(".jack", "T.xml");
        string expectedOutput = File.ReadAllText(expectedOutputPath);

        var sb = new StringBuilder();
        sb.AppendLine("<tokens>\r");
        foreach (var token in tokens)
        {
            if (token.Type == SyntaxAnalyzer.TokenType.EOF)
            {
                break;
            }

            sb.Append(token.ToString());
        }
        sb.AppendLine("</tokens>\r");

        Assert.Equal(expectedOutput, sb.ToString());
    }


}