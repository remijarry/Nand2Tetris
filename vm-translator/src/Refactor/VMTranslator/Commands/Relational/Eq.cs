namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Equals
    /// </summary>
    public class Eq : RelationalCommand
    {

        protected override string GetFunctionName()
        {
            return "(LT)";
        }

        protected override string GetJumpCondition()
        {
            return "D;JEQ";
        }
    }
}