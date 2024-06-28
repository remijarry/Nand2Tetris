namespace assembler;

public class SymbolTable
{

  /// <summary>
  /// Symbol table initialised with the predefined symbols and their associated address numbers
  /// </summary>
  /// <returns></returns>
  private readonly Dictionary<string, string> _table = new()
    {
      {"R0","0"},
      {"SP","0"},
      {"R1","1"},
      {"LCL","1"},
      {"R2","2"},
      {"ARG","2"},
      {"R3","3"},
      {"THIS","3"},
      {"R4","4"},
      {"THAT","4"},
      {"R5","5"},
      {"R6","6"},
      {"R7","7"},
      {"R8","8"},
      {"R9","9"},
      {"R10","10"},
      {"R11","11"},
      {"R12","12"},
      {"R13","13"},
      {"R14","14"},
      {"R15","15"},
      {"KBD","24576"},
      {"SCREEN","16384"},
    };


  public SymbolTable()
  {

  }

  public void Add(string symbol, string value)
  {
    if (string.IsNullOrEmpty(symbol))
      return;

    if (string.IsNullOrEmpty(value))
      return;

    if (_table.ContainsKey(symbol))
      return;

    _table[symbol] = value.ToString();
  }

  public string GetAddress(string symbol)
  {
    if (string.IsNullOrWhiteSpace(symbol))
      return string.Empty;

    _table.TryGetValue(symbol, out string? value);

    if (value == null)
      return string.Empty;

    return value;
  }

  public bool Contains(string symbol)
  {
    if (string.IsNullOrWhiteSpace(symbol))
      return false;

    return _table.ContainsKey(symbol);
  }
}

