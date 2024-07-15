namespace VMTranslator.Enums
{
  public enum VirtualSegment
  {
    CONSTANT, // uses stack pointer
    LOCAL,
    ARGUMENT,
    TEMP,
    THIS,
    THAT,
    POINTER,
  }
}