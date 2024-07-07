
using System;
using System.Text;
using VMTranslator.Commands;
using VMTranslator.Enums;

namespace VMTranslator
{
  public class CodeWriter
  {
    private readonly string _fileName;
    private StringBuilder sb = new();
    private ICommand _pushCommand;

    public CodeWriter(string fileName)
    {
      _fileName = fileName;
      _pushCommand = new PushCommand();
    }

    public string WritePushPop(Command command, VirtualSegment segment, int index)
    {
      if (index < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(index));
      }


      switch (command)
      {
        case Command.C_POP:
          return string.Empty;
        case Command.C_PUSH:
          _pushCommand.Segment = segment;
          _pushCommand.Index = index;
          return _pushCommand.Execute();
        default:
          throw new Exception("No valid command provided");
      }
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