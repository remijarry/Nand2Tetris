using VMTranslator.Commands.Stack;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Memory
{
    public class Local : MemoryCommand
    {
        public override string Segment => MemorySegment.LOCAL;
        public Local(StackOperation stackOperation, string index) : base(stackOperation, index)
        {
        }
    }
}