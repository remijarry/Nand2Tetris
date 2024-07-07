
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

    public string WritePushCommand(MemoryAccessCommand command)
    {
      StringBuilder sb = new();
      sb.AppendLine($"// {command.Action} {command.Segment} {command.Index}");

      sb.AppendLine($"@{command.Index}");
      sb.AppendLine("D=A");

      #region use the stack pointer to select ram address (RAM[RAM[0]])
      sb.AppendLine($"@{_segmentManager.GetStackPointerBaseAddress}");
      sb.AppendLine("A=M");
      #endregion

      // set memory address to index
      sb.AppendLine("M=D");
      // increment stack pointer
      sb.AppendLine("@0");
      sb.AppendLine("M=M+1");

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

    private string WriteArithmetic(ArithmeticCommand command)
    {
      throw new NotImplementedException();
    }

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