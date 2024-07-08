using System;

namespace VMTranslator.Commands
{
  public class CompCommands
  {
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

    /// <summary>
    /// D=-D
    /// </summary>
    /// <returns></returns>
    public static string NegateDRegisterValue()
    {
      return "D=-D";
    }
  }
}