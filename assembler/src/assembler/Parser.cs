using System.Text;
using System.Text.RegularExpressions;

namespace assembler;

public class Parser
{

  /// <summary>
  /// The file to process
  /// </summary>
  private readonly StreamReader _streamReader;

  /// <summary>
  /// Pointer to the next available memory address for variables. We start at 16 onwards
  /// </summary>
  private int _variableNumber = 16;
  private SymbolTable _symbolTable = new();
  private readonly Code codes = new();

  public Parser(StreamReader streamReader)
  {
    _streamReader = streamReader;
  }

  /// <summary>
  /// 2 pass parsing. The first pass fills the symbol table with the labels.
  /// The second pass deals with the variables and converts the code to binary representation.
  /// /// </summary>
  /// <returns></returns>
  public string Parse()
  {
    var result = new StringBuilder();
    // first pass
    ParseLabels();
    // second pass
    result.Append(ConvertToBinary());
    return result.ToString();
  }

  /// <summary>
  /// First pass, deals only with the (LABELS)
  /// </summary>
  public void ParseLabels()
  {
    int lineNumber = 0;
    var line = string.Empty;
    while ((line = _streamReader.ReadLine()) != null)
    {

      if (IsComment(line))
        continue;

      line = NormalizeString(line);

      if (string.IsNullOrEmpty(line))
        continue;

      if (IsLabel(line))
      {
        // (LOOP)
        // remove the parenthesis because the variable will reference the label without them
        // @LOOP
        var label = line[1..^1];
        if (_symbolTable.Contains(label))
        {
          // should not already contain it but let's not throw an error
          continue;
        }

        _symbolTable.Add(label, lineNumber.ToString());
        // we don't increment the counter when dealing with a labels
        continue;
      }

      lineNumber++;
    }

    // reset the pointer to the beginning of the file to prepare it for the second pass.
    _streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
  }

  /// <summary>
  /// Second pass, deals with the variables and convert the instructions to their binary representation
  /// </summary>
  /// <returns></returns>
  public string ConvertToBinary()
  {
    var result = new StringBuilder();

    string? line;
    while ((line = _streamReader.ReadLine()) != null)
    {
      if (IsComment(line))
        continue;

      line = NormalizeString(line);

      if (string.IsNullOrEmpty(line))
        continue;

      if (line.StartsWith('(') && line.EndsWith(')'))
        continue;

      if (line.StartsWith('@'))
      {
        // remove the @ symbol from the instruction
        result.AppendLine(GetAInstruction(line[1..]));
      }
      else
      {
        result.AppendLine(GetCInstruction(line));
      }
    }

    // remove the last \n
    result.Length--;
    return result.ToString();
  }

  public string GetAInstruction(string instruction)
  {
    bool isNumber = int.TryParse(instruction, out int value);
    if (isNumber)
    {
      return codes.GetAInstruction(instruction);
    }
    // first time we encounter the instruction
    if (!_symbolTable.Contains(instruction))
    {
      _symbolTable.Add(instruction, _variableNumber.ToString());
      _variableNumber++;
    }

    return codes.GetAInstruction(_symbolTable.GetAddress(instruction));
  }

  public string GetCInstruction(string instruction)
  {
    Regex rgx = new(@"^([AMD]+=)?([01ADM!\-+&|]+)(;([J][A-Z]+))?$");

    var match = rgx.Match(instruction);

    if (!match.Success)
    {
      return string.Empty;
    }

    string dest = match.Groups[1].Success ? match.Groups[1].Value : "null";
    string comp = match.Groups[2].Value;
    string jump = match.Groups[4].Success ? match.Groups[4].Value : "null";

    // remove the = in D= or A= or M=
    if (dest != "null")
    {
      dest = dest[..^1];
    }

    return codes.GetCInstruction(dest, comp, jump);
  }
  public bool IsComment(string line)
  {
    return line.StartsWith('/');
  }
  public bool IsLabel(string line)
  {
    return line.StartsWith('(') && line.EndsWith(')');
  }

  /// <summary>
  /// Removes the comments and extra spaces from the string
  /// </summary>
  /// <returns></returns>
  public string NormalizeString(string str)
  {
    if (string.IsNullOrWhiteSpace(str))
      return string.Empty;

    int index = str.IndexOf("//");
    if (index == -1)
    {
      return str.Trim();
    }
    else
    {
      return str[..index].Trim();
    }
  }
}