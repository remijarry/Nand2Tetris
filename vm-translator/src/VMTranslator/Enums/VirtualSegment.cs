namespace VMTranslator.Enums
{
  public enum VirtualSegment
  {
    CONSTANT, // uses stack pointer
    LOCAL,
    ARG,
    THIS,
    THAT
  }
}