using System.Text;

namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Greater than
    /// </summary>
    public class Gt : RelationalCommand, ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            return GetAsm(sb);
        }

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