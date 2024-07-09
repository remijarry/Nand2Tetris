// push constant 2
@2
D=A
@0
A=M
M=D
@0
M=M+1

// push constant 3
@3
D=A
@0
A=M
M=D
@0
M=M+1

// gt
@0
M=M-1
@0
M=M-1
@0
A=M
D=M
@0
M=M+1
@0
A=M
D=D-M
@0
M=M-1
@GREATER
D;JGT
@NOT_GREATER
0;JMP
(GREATER)
@0
A=M
D=-1
M=D
@END_GREATER
0;JMP
(NOT_GREATER)
@0
A=M
D=M
D=0
M=D
(END_GREATER)
@0
M=M+1

