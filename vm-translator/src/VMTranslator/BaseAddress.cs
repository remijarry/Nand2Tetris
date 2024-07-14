namespace VMTranslator
{
  public static class BaseAddress
  {
    /// <summary>
    /// Stack pointer
    /// </summary>
    public const int SP = 0;

    /// <summary>
    /// LOCAL
    /// </summary>
    public const int LCL = 1;
    /// <summary>
    /// ARGUMENTS
    /// </summary>
    public const int ARG = 2;
    /// <summary>
    /// THIS segment within the heap
    /// </summary>
    public const int THIS = 3;
    /// <summary>
    /// THAT segment within the heap
    /// </summary>
    public const int THAT = 4;
    public const int TEMP = 5;
  }
}