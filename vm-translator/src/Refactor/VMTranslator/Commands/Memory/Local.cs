using VMTranslator.Commands.Stack;

namespace VMTranslator.Commands.Memory
{
    public class Local : MemoryCommand
    {
        public Local(StackOperation stackOperation, string index) : base(stackOperation, index)
        {
        }
    }
}