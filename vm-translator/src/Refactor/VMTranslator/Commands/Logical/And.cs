namespace VMTranslator.Commands.Logical
{
    public class And : ICommand
    {
        public string Execute()
        {
            return
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
                "A=M";


        }
    }
}

// $"@{Constants.StackPointer}",
//                 "AM=M-1",
//                 "D=M",
//                 "@R13",
//                 "M=D",
//                 $"@{Constants.StackPointer}",
//                 "A=M-1",
//                 "D=M",
//                 "@R13",
//                 "A=M",
//                 "D=D&A",
//                 $"@{Constants.StackPointer}",
//                 "A=M-1",
//                 "M=D"