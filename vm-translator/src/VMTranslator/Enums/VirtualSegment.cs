namespace VMTranslator.Enums
{
  public enum VirtualSegment
  {
    ARGUMENT,
    CONSTANT, // uses stack pointer
    LOCAL,
    TEMP,
    THAT,
    THIS,
    POINTER,
    STATIC
  }
}