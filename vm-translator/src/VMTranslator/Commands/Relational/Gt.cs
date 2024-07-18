using System.Text;

namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Greater than
    /// </summary>
    public class Gt : RelationalCommand
    {
        protected override string GetFunctionName()
        {
            return "(GT)";
        }

        protected override string GetJumpCondition()
        {
            return "D;JGT";
        }
    }
}