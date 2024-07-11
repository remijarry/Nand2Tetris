using System;
using System.Collections.Generic;
using VMTranslator.Commands;
using VMTranslator.Functions;

namespace VMTranslator
{
  public class ParsedFile
  {
    public CommandList Commands { get; set; } = new CommandList();

    /// <summary>
    /// Contains the list of arithmetic functions to create before translating the commands to assembly
    /// </summary>
    /// <value></value>
    private HashSet<string> ArithmeticFunctions { get; set; } = new HashSet<string>();

    public List<IFunction> Functions { get; } = new List<IFunction>();

    public void AddFunction(string function)
    {
      if (!ArithmeticFunctions.Contains(function))
      {
        Functions.Add(new Function(function));
        ArithmeticFunctions.Add(function);
      }
    }
  }
}