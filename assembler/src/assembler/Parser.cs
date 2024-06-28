using System.Text;

namespace assembler;

public class Parser
{

  /// <summary>
  /// The file to process
  /// </summary>
  private readonly StreamReader _streamReader;

  /// <summary>
  /// Track the line number of the currently processed line.
  /// When a (LABEL) is encountered, lineNumber is not incremented.
  /// When a //comment is encountered, lineNumber is not incremented.
  /// </summary>
  private int _labelLineNumber;

  private SymbolTable _symbolTable = new();

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
    ParseLabels();
    // result = SecondPass();


    return result.ToString();
  }

  /// <summary>
  /// First pass, deals only with the (LABELS)
  /// </summary>
  public void ParseLabels()
  {
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

        _symbolTable.Add(label, _labelLineNumber.ToString());
        // we don't increment the counter when dealing with a label
        continue;
      }

      _labelLineNumber++;
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



    return result.ToString();
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