namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Less than
    /// </summary>
    public class Lt : RelationalCommand
    {
        protected override string GetFunctionName()
        {
            return "(LT)";
        }

        protected override string GetJumpCondition()
        {
            return "D;JLT";
        }
    }
}