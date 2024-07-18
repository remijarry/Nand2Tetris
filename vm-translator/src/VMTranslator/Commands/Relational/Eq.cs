namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Equals
    /// </summary>
    public class Eq : RelationalCommand
    {

        protected override string GetFunctionName()
        {
            return "(EQ)";
        }

        protected override string GetJumpCondition()
        {
            return "D;JEQ";
        }
    }
}