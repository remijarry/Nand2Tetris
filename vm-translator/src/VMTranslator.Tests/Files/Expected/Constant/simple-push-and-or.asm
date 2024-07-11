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

// or
@RETURN_OR_2
D=A
@R5
M=D
@OR
D=A
0;JMP
(RETURN_OR_2)
@0
M=M+1


(OR)
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
D=D|M
@0
M=M-1
@0
A=M
M=D
@R5
A=M
D=M
0;JMP
(TRUE)
@0
A=M
D=-1
M=D
@R5
A=M
D=M
0;JMP
(FALSE)
@0
A=M
D=0
M=D
@R5
A=M
D=M
0;JMP
