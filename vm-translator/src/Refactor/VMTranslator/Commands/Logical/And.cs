using System;
using System.Text;
using VMTranslator.Constants;

namespace VMTranslator.Commands.Logical
{
    public class And : ICommand
    {
        public string Execute()
        {
            return
                "(AND)" +
                $"@{Pointers.STACK}" +
                "M=M-1" +
                $"@{Pointers.STACK}" +
                "M=M-1" +
                $"@{Pointers.STACK}" +
                "A=M" +
                "D=M" +
                $"@{Pointers.STACK}" +
                "M=M+1" +
                $"@{Pointers.STACK}" +
                "A=M" +
                "D=D&M" +
                $"@{Pointers.STACK}" +
                "M=M-1" +
                $"@{Pointers.STACK}" +
                "A=M" +
                "M=D";
        }

        public StringBuilder Execute(StringBuilder sb)
        {
            throw new NotImplementedException();
        }
    }
}