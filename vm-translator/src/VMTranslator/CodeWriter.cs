
using System;
using System.Text;
using VMTranslator.Commands;
using VMTranslator.Segments;
using Action = VMTranslator.Enums.Action;

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
      if (command.Action == Action.push)
      {
        return WritePushCommand(command);
      }
      else if (command.Action == Action.pop)
      {
        return WritePopCommand(command);
      }
      else
      {
        throw new NotSupportedException();
      }
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
      sb.AppendLine($"// {command.Action} {command.Segment} {command.Index}".ToLower());
      sb.AppendLine(SetDRegisterToIndex(command.Index));
      sb.AppendLine(SelectStackPointerMemoryValue());
      sb.AppendLine(SetMemoryValueFrom("D"));
      sb.AppendLine(IncrementMemoryStackPointer());
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
      sb.AppendLine($"// {command.Type}");

      // decrement stack pointer by 2 because x has to be the left operand.
      sb.AppendLine(DecrementMemoryStackPointer());
      sb.AppendLine(DecrementMemoryStackPointer());

      sb.AppendLine(SelectStackPointerMemoryValue());
      sb.AppendLine(SetDRegistertoRamValue());

      sb.AppendLine(IncrementMemoryStackPointer());
      sb.AppendLine(SelectStackPointerMemoryValue());

      switch (command.Type)
      {
        case ArithmeticCommandType.add:
          sb.AppendLine(AddDRegisterToRamValue());
          sb.AppendLine(DecrementMemoryStackPointer());
          break;
        case ArithmeticCommandType.sub:
          sb.AppendLine(SubDRegisterToRamValue());
          sb.AppendLine(DecrementMemoryStackPointer());
          break;

      }

      sb.AppendLine(SelectStackPointerMemoryValue());
      sb.AppendLine(SetRamValueToDRegister());
      sb.AppendLine(IncrementMemoryStackPointer());

      _segmentManager.DecrementPointerBy(1);
      return sb.ToString();
    }

    #region assembly code translation methods

    private string SetDRegisterToIndex(int index)
    {
      return $"@{index}{Environment.NewLine}D=A";
    }

    /// <summary>
    /// D=D+M
    /// </summary>
    /// <returns></returns>
    private string SetDRegistertoRamValue()
    {
      return "D=M";
    }

    private string AddDRegisterToRamValue()
    {
      return "D=D+M";
    }

    /// <summary>
    /// D=D-M
    /// </summary>
    /// <returns></returns>
    private string SubDRegisterToRamValue()
    {
      return "D=D-M";
    }

    /// <summary>
    /// M=D
    /// </summary>
    /// <returns></returns>
    private string SetRamValueToDRegister()
    {
      return "M=D";
    }

    /// <summary>
    /// @0;A=M
    /// </summary>
    /// <returns></returns>
    private string SelectStackPointerMemoryValue()
    {
      return $"@{_segmentManager.GetStackPointerBaseAddress}{Environment.NewLine}A=M";
    }

    private string SetMemoryValueFrom(string register)
    {
      return $"M={register}";
    }

    private string IncrementMemoryStackPointer()
    {
      return $"@0{Environment.NewLine}M=M+1";
    }

    private string DecrementMemoryStackPointer()
    {
      return $"@0{Environment.NewLine}M=M-1";
    }

    #endregion



    public void SetFileName(string fileName)
    {
      // todo
    }

    /// <summary>
    /// RAM[SP++] = index
    /// </summary>
    /// <param name="index">The constant value to push</param>
    private void WritePushConstant(int index)
    {
      sb.AppendLine($"// push constant {index}");
      // @index
      sb.AppendLine($"@{index}");
      sb.AppendLine("D=A");
      // @SP (base address is 0)
      sb.AppendLine("@0");
      sb.AppendLine("A=M");
      sb.AppendLine("M=D");
      sb.AppendLine("@0");
      sb.AppendLine("M=M+1");

      // incrementing the stack pointer by 1
      // SP++;
    }
  }
}