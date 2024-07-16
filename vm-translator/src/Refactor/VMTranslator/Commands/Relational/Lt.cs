using System.Text;

namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Less than
    /// </summary>
    public class Lt : RelationalCommand, ICommand
    {
        public StringBuilder Execute(StringBuilder sb)
        {
            return GetAsm(sb);
        }

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