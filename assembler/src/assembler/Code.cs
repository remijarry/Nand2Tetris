using System.Text;

namespace assembler;

public class Code
{
  private const int BINARY_INSTRUCTION_LENGTH = 16;
  private const string A_INSTRUCTION_PREFIX = "0";
  private const int A_INSTRUCTION_LENGTH = 1;
  private const int C_INSTRUCTION_LENGTH = 3;
  private const string C_INSTRUCTION_PREFIX = "111";
  private const string NO_DESTINATION = "null";
  private const string COMP_MNEMONIC_WHEN_M = "1";
  private const string COMP_MNEMONIC_WHEN_A = "0";

  /// <summary>
  /// Used with C instructions. Stores the Comp mapping (bits 12 to 6)
  /// </summary>
  /// <returns></returns>
  private readonly Dictionary<string, string> _cTable = new()
  {
    {"0","101010"},
    {"1","111111"},
    {"-1","111010"},
    {"D","001100"},
    {"A","110000"},
    {"M","110000"},
    {"!D","001101"},
    {"!A","110001"},
    {"!M","110001"},
    {"-D","001111"},
    {"-A","110011"},
    {"-M","110011"},
    {"D+1","011111"},
    {"A+1","110111"},
    {"M+1","110111"},
    {"D-1","001110"},
    {"A-1","110010"},
    {"M-1","110010"},
    {"D+A","000010"},
    {"D+M","000010"},
    {"D-A","010011"},
    {"D-M","010011"},
    {"A-D","000111"},
    {"M-D","000111"},
    {"D&A","000000"},
    {"D&M","000000"},
    {"D|A","010101"},
    {"D|M","010101"},
  };

  /// <summary>
  /// Used with C instructions. Stores the Dest mapping (bits 5 to 3)
  /// </summary>
  /// <returns></returns>
  private readonly Dictionary<string, string> _dTable = new()
  {
    {"null","000"},
    {"M","001"},
    {"D","010"},
    {"MD","011"},
    {"DM","011"},
    {"A","100"},
    {"AM","101"},
    {"AD","110"},
    {"AMD","111"},
    {"ADM","111"},
  };

  /// <summary>
  /// Used with C instructions. Stores the Jump mapping (bits 2 to 0)
  /// </summary>
  /// <returns></returns>
  private readonly Dictionary<string, string> _jTable = new()
  {
    {"null","000"},
    {"JGT","001"},
    {"JEQ","010"},
    {"JGE","011"},
    {"JLT","100"},
    {"JNE","101"},
    {"JLE","110"},
    {"JMP","111"},
  };

  /// <summary>
  /// Given a 16 bits instruction,
  /// A instructions start with 0.
  /// The following 2 zeroes are not used as instructions but appended to the returned string.
  /// </summary>
  /// <returns></returns>
  public string GetAInstruction(string instruction)
  {
    var binaryRepresentation = DecimalStringToBinaryString(instruction);
    var padLeft = BINARY_INSTRUCTION_LENGTH - A_INSTRUCTION_LENGTH;
    return $"{A_INSTRUCTION_PREFIX}{binaryRepresentation.PadLeft(padLeft, '0')}";
  }

  /// <summary>
  /// dest=comp;jmp
  /// Given a 16 bits instruction,
  /// C instructions start with 1.
  /// The following 2 zeroes are not used as instructions but appended to the returned string.
  /// </summary>
  /// <returns></returns>
  public string GetCInstruction(string dest, string comp, string jump)
  {
    var compMnemonic = comp.Contains('M') ? COMP_MNEMONIC_WHEN_M : COMP_MNEMONIC_WHEN_A;

    _cTable.TryGetValue(comp, out string? c);
    _dTable.TryGetValue(dest, out string? d);
    _jTable.TryGetValue(jump, out string? j);

    return $"{C_INSTRUCTION_PREFIX}{compMnemonic}{c}{d}{j}";
  }
  private string DecimalStringToBinaryString(string number)
  {
    int n = int.Parse(number);
    if (n == 0)
    {
      return "0";
    }

    var result = new StringBuilder();

    while (n > 0)
    {
      int remainder = n % 2;
      result.Insert(0, remainder);
      n /= 2;
    }

    return result.ToString();
  }
}