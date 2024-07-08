
using System;
using System.Text;
using VMTranslator.Commands;
using VMTranslator.Enums;
using VMTranslator.Segments;

namespace VMTranslator
{
  public class CodeWriter
  {
    private readonly string _fileName;
    private StringBuilder sb = new();
    private SegmentManager _segmentManager;


    public CodeWriter(string fileName)
    {
      _fileName = fileName;
      _segmentManager = new SegmentManager();

    }

    public string WriteCommand(ICommand command)
    {
      switch (command)
      {
        case MemoryAccessCommand memoryCommand:
          return WritePushPop(memoryCommand);
        case ArithmeticCommand arithmeticCommand:
          return WriteArithmetic(arithmeticCommand);
        default:
          throw new NotSupportedException();
      }
    }

    private string WritePushPop(MemoryAccessCommand command)
    {
      var code = command.GetAssemblyCode();
      _segmentManager.IncrementPointer(command.Segment);
      return code;
    }

    /// <summary>
    /// ex: push constant 2
    /// @2
    ///D=A
    ///@0
    ///A=M
    ///M=D
    ///@0
    ///M=M+1
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public string WritePushCommand(MemoryAccessCommand command)
    {
      StringBuilder sb = new();

      _segmentManager.IncrementPointer(command.Segment);

      return sb.ToString();
    }

    public string WritePopCommand(MemoryAccessCommand command)
    {
      StringBuilder sb = new();


      // decrement stack pointer
      sb.AppendLine("@0");
      sb.AppendLine("M=M+1");
      return sb.ToString();
    }

    /// <summary>
    /// Pops 2 values from the stack, performs the arithmetic operation and push the result back onto the stack.
    /// if stack is [x, y] then we do x + y / x - y etc...
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    private string WriteArithmetic(ArithmeticCommand command)
    {

      var sb = new StringBuilder();
      sb.Append(command.GetAssemblyCode());
      switch (command.Name)
      {
        // we only have one value left out of two.
        case CommandName.add:
        case CommandName.sub:
          _segmentManager.DecrementPointerBy(1);
          break;
        // y has been negated and put back on the stack. we don't touch the pointer.
        case CommandName.neg:
          break;
      }

      return sb.ToString();
    }
    public void SetFileName(string fileName)
    {
      // todo
    }
  }
}