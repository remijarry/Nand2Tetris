using System.Collections.Generic;
using VMTranslator.Enums;

namespace VMTranslator.Segments
{
  public class SegmentManager
  {
    private Dictionary<VirtualSegment, int> pointers;

    public SegmentManager()
    {
      pointers = new Dictionary<VirtualSegment, int>
      {
        {VirtualSegment.Constant, 256},
        // {"SP", 256},
        // {"LCL", 0},
        // {"ARG", 0},
        // {"THIS", 0},
        // {"THAT", 0},
        //todo: temp segment RAM[5-12]
        //todo: general purpose registers RAM[13-15]
      };
    }

    public void IncrementPointer(VirtualSegment segment)
    {
      if (pointers.ContainsKey(segment))
      {
        pointers[segment]++;
      }
      else
      {
        // handle error
      }
    }

    public int GetStackPointerBaseAddress => 0;
  }
}