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
@RETURN_ADD_0
D=A
@R5
M=D
@ADD
D=A
0;JMP
(RETURN_ADD_0)
@0
M=M+1
(ADD)
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
@R5
A=M
D=M
0;JMP


x = A
y = D

x = x + y
y = x - y
x = x - y

A=D+A
D=A-D
A=A-D
