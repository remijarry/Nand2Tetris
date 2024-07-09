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

// lt
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
@LESS_THAN
D;JLT
@NOT_LESS_THAN
0;JMP
(LESS_THAN)
@0
A=M
D=-1
M=D
@END_LESS_THAN
0;JMP
(NOT_LESS_THAN)
@0
A=M
D=M
D=0
M=D
(END_LESS_THAN)
@0
M=M+1

