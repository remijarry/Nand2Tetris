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

// eq
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
@EQUAL
D;JEQ
@NOT_EQUAL
0;JMP
(EQUAL)
@0
A=M
D=D-1
M=D
@END_EQUAL
0;JMP
(NOT_EQUAL)
@0
A=M
D=M
D=0
M=D
(END_EQUAL)
@0
M=M+1

