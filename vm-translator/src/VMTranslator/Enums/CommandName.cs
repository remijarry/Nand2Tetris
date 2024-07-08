namespace VMTranslator.Enums
{
  public enum CommandName
  {
    push,
    pop,
    add,
    sub,
    neg,
    // true if x = y
    eq,
    // true if x > y
    gt,
    // true if x < y
    lt,
    // x and y
    and,
    // x or y
    or,
    // not y
    not
  }
}
