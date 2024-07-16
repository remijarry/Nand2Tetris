using System.Text;

namespace VMTranslator.Commands.Relational
{
    /// <summary>
    /// Equals
    /// </summary>
    public class Eq : RelationalCommand, ICommand
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
            return "D;JEQ";
        }
    }
}