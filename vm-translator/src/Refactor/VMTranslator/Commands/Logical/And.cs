using System;
using System.Text;

namespace VMTranslator.Commands.Logical
{
    public class And : ICommand
    {
        public string Execute()
        {
            return
                "(AND)" +
                $"@{Constants.StackPointer}" +
                "M=M-1" +
                $"@{Constants.StackPointer}" +
                "M=M-1" +
                $"@{Constants.StackPointer}" +
                "A=M" +
                "D=M" +
                $"@{Constants.StackPointer}" +
                "M=M+1" +
                $"@{Constants.StackPointer}" +
                "A=M" +
                "D=D&M" +
                $"@{Constants.StackPointer}" +
                "M=M-1" +
                $"@{Constants.StackPointer}" +
                "A=M" +
                "M=D";
        }

        public StringBuilder Execute(StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}