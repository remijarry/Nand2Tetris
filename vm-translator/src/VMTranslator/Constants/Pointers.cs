using System.Collections.Generic;

namespace VMTranslator.Constants
{
  public static class Pointers
  {
    public const int STACK = 0;
    public const int LOCAL = 1;
    public const int ARGUMENT = 2;
    public const int THIS = 3;
    public const int THAT = 4;
    public const int TEMP = 5;

    public static readonly Dictionary<string, int> SegmentBaseAddress = new Dictionary<string, int>()
    {
      {MemorySegment.CONSTANT, STACK},
      {MemorySegment.LOCAL, LOCAL},
      {MemorySegment.ARGUMENT, ARGUMENT},
      {MemorySegment.THIS, THIS},
      {MemorySegment.THAT, THAT},
      {MemorySegment.TEMP, TEMP},
    };
    public const string R13 = "R13";
  }

}