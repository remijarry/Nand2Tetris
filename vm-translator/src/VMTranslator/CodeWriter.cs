using System;
using System.Collections.Generic;
using System.Text;
using VMTranslator.Commands;

namespace VMTranslator
{
  public class CodeWriter
  {
    private readonly string _fileName;
    private StringBuilder sb = new();

    /// <summary>
    /// Static pointer initial value starts at 16 onwards
    /// </summary>
    public static int StaticPointer = 16;
    /// <summary>
    /// Keeps track of the index to pointer.
    /// pop static 8 will pop the value from stack and store it at Mem[StaticPointer++].
    /// to be able to know where to look when we get a push static 8 we need to store the index (8) as a key and the memory address as a value
    /// </summary>
    /// <typeparam name="int"></typeparam>
    /// <typeparam name="int"></typeparam>
    /// <returns></returns>
    public static Dictionary<int, int> IndexToMemAddr = new Dictionary<int, int>();

    public CodeWriter(string fileName)
    {
      _fileName = fileName;
    }

    /// <summary>
    /// Writes all the arithmetic functions we need at the end of the file.
    /// </summary>
    /// <param name="function"></param>
    /// <returns></returns>
    public string WriteFunction(IFunction function)
    {
      return function.GetAssemblyCode();
    }

    public string WriteCommand(ICommand command)
    {
      return command.GetAssemblyCode();
    }

    public string WritePopCommand(MemoryAccessCommand command)
    {
      StringBuilder sb = new();
      // decrement stack pointer
      sb.AppendLine("@0");
      sb.AppendLine("M=M+1");
      return sb.ToString();
    }

    public void SetFileName(string fileName)
    {
      // todo
    }
  }
}