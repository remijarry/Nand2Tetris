namespace VMTranslator.Constants
{
  public static class CommandName
  {
    #region arithmetic
    public const string ADD = "add";
    public const string NEG = "neg";
    public const string SUB = "sub";
    #endregion

    #region logical
    public const string AND = "and";
    public const string NOT = "not";
    public const string OR = "or";
    #endregion

    #region relational
    public const string EQ = "eq";
    public const string GT = "gt";
    public const string LT = "lt";
    #endregion

    #region stack
    public const string PUSH = "push";
    public const string POP = "pop";
    #endregion
  }
}