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

// add
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
D=D+M
@0
M=M-1
@0
A=M
M=D
@0
M=M+1

// push constant 4
@4
D=A
@0
A=M
M=D
@0
M=M+1

// sub
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
@0
A=M
M=D
@0
M=M+1

// push constant 5
@5
D=A
@0
A=M
M=D
@0
M=M+1

// neg
@0
M=M-1
@0
A=M
D=M
D=-D
M=D
@0
M=M+1

