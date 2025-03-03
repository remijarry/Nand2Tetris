using System.ComponentModel;

namespace JackAnalyzer.Scan;

public enum LexicalElement
{

    [Description("EndOfFile")]
    EOF,

    [Description("Keyword")]
    KEYWORD,

    [Description("Symbol")]
    SYMBOL,

    [Description("IntegerConstant")]
    INTEGER_CONSTANT,

    [Description("StringConstant")]
    STRING_CONSTANT,

    [Description("Identifier")]
    IDENTIFIER,
}