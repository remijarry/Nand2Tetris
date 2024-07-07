namespace VMTranslator.Commands
{
  public class ArithmeticCommand : ICommand
  {
    public string CommandType => "arithmetic";

    public ArithmeticCommandType Type { get; private set; }

    public ArithmeticCommand(ArithmeticCommandType type)
    {
      Type = type;
    }
  }

  public enum ArithmeticCommandType
  {
    add,
    sub,
    neg,
    eq,
    gt,
    lt,
    and,
    or,
    not
  }

}