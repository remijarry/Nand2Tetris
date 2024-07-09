using System;
using System.Text;

namespace VMTranslator.Commands
{
  //todo: maybe split this into d instructions, j instructions etc.
  public class CompCommands
  {
    #region J instructions
    /// <summary>
    /// 0;JMP
    /// </summary>
    /// <returns></returns>
    public static string JumpUnconditional()
    {
      return "0;JMP";
    }

    /// <summary>
    /// val;JEQ
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string JumpEqual(string val)
    {
      return $"{val};JEQ";
    }
    #endregion

    /// <summary>
    /// @LABEL
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    public static string ReferenceLabel(string label)
    {
      return $"@{label.ToUpper()}";
    }

    /// <summary>
    /// (LABEL)
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    public static string CreateLabel(string label)
    {
      return $"({label.ToUpper()})";
    }

    #region D=
    /// <summary>
    /// @i;D=A
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public static string SetDRegisterToIndex(int index)
    {
      return $"@{index}{Environment.NewLine}D=A";
    }

    /// <summary>
    /// D=M
    /// </summary>
    /// <returns></returns>
    public static string SetDRegistertoRamValue()
    {
      return "D=M";
    }

    /// <summary>
    /// D=D+M
    /// </summary>
    /// <returns></returns>
    public static string AddDRegisterToRamValue()
    {
      return "D=D+M";
    }

    /// <summary>
    /// D=D-M
    /// </summary>
    /// <returns></returns>
    public static string SubDRegisterToRamValue()
    {
      return "D=D-M";
    }

    /// <summary>
    /// D=-D
    /// </summary>
    /// <returns></returns>
    public static string NegateDRegisterValue()
    {
      return "D=-D";
    }

    /// <summary>
    /// D=D-1
    /// </summary>
    /// <returns></returns>
    public static string SubOneToD()
    {
      return "D=D-1";
    }

    /// <summary>
    /// D=0
    /// </summary>
    /// <returns></returns>
    public static string SetDToZero()
    {
      return "D=0";
    }

    #endregion

    /// <summary>
    /// M=D
    /// </summary>
    /// <returns></returns>
    public static string SetRamValueToDRegister()
    {
      return "M=D";
    }

    /// <summary>
    /// @0;A=M
    /// </summary>
    /// <returns></returns>
    public static string SelectStackPointerMemoryValue()
    {
      return $"@0{Environment.NewLine}A=M";
    }

    /// <summary>
    /// M=A or M=D
    /// </summary>
    /// <param name="register"></param>
    /// <returns></returns>
    public static string SetMemoryValueFrom(string register)
    {
      return $"M={register}";
    }

    /// <summary>
    /// @0;M=M+1
    /// </summary>
    /// <returns></returns>
    public static string IncrementMemoryStackPointer()
    {
      return $"@0{Environment.NewLine}M=M+1";
    }

    /// <summary>
    /// @0;M=M-1
    /// </summary>
    /// <returns></returns>
    public static string DecrementMemoryStackPointer()
    {
      return $"@0{Environment.NewLine}M=M-1";
    }

    public static string SelectXAndYFromTheStack()
    {
      var sb = new StringBuilder();
      sb.AppendLine(DecrementMemoryStackPointer());
      sb.AppendLine(DecrementMemoryStackPointer());
      sb.AppendLine(SelectStackPointerMemoryValue());
      sb.AppendLine(SetDRegistertoRamValue());
      sb.AppendLine(IncrementMemoryStackPointer());
      sb.AppendLine(SelectStackPointerMemoryValue());
      return sb.ToString();
    }
  }
}