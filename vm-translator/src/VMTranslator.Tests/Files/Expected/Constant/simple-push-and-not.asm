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

// not
@0
M=M-1
@0
A=M
D=M
D=!D
D=D+1
@0
A=M
M=D
@0
M=M+1

