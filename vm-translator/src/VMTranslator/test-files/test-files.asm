// push CONSTANT 10
@10
D=A
@0
A=M
M=D
@0
M=M+1
// pop local 0
@0
M=M-1
@1
A=M
D=A
@0
D=D+A
@0
A=M
D=D+M
A=D-M
D=D-A
M=D
// push CONSTANT 21
@21
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 22
@22
D=A
@0
A=M
M=D
@0
M=M+1
// pop argument 2
@0
M=M-1
@2
A=M
D=A
@2
D=D+A
@0
A=M
D=D+M
A=D-M
D=D-A
M=D
// pop argument 1
@0
M=M-1
@2
A=M
D=A
@1
D=D+A
@0
A=M
D=D+M
A=D-M
D=D-A
M=D
// push CONSTANT 36
@36
D=A
@0
A=M
M=D
@0
M=M+1
// pop this 6
@0
M=M-1
@3
A=M
D=A
@6
D=D+A
@0
A=M
D=D+M
A=D-M
D=D-A
M=D
// push CONSTANT 42
@42
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 45
@45
D=A
@0
A=M
M=D
@0
M=M+1
// pop that 5
@0
M=M-1
@4
A=M
D=A
@5
D=D+A
@0
A=M
D=D+M
A=D-M
D=D-A
M=D
// pop that 2
@0
M=M-1
@4
A=M
D=A
@2
D=D+A
@0
A=M
D=D+M
A=D-M
D=D-A
M=D
// push CONSTANT 510
@510
D=A
@0
A=M
M=D
@0
M=M+1
// pop temp 6
@0
M=M-1
@11
D=A
@0
A=M
D=D+M
A=D-M
D=D-A
M=D