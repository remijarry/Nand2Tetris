// push CONSTANT 17
@17
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 17
@17
D=A
@0
A=M
M=D
@0
M=M+1
// eq
@RETURN_EQ_2
D=A
@R13
M=D
@EQ
D=A
0;JMP
(RETURN_EQ_2)
@0
M=M+1
// push CONSTANT 17
@17
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 16
@16
D=A
@0
A=M
M=D
@0
M=M+1
// eq
@RETURN_EQ_4
D=A
@R13
M=D
@EQ
D=A
0;JMP
(RETURN_EQ_4)
@0
M=M+1
// push CONSTANT 16
@16
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 17
@17
D=A
@0
A=M
M=D
@0
M=M+1
// eq
@RETURN_EQ_6
D=A
@R13
M=D
@EQ
D=A
0;JMP
(RETURN_EQ_6)
@0
M=M+1
// push CONSTANT 892
@892
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 891
@891
D=A
@0
A=M
M=D
@0
M=M+1
// lt
@RETURN_LT_8
D=A
@R13
M=D
@LT
D=A
0;JMP
(RETURN_LT_8)
@0
M=M+1
// push CONSTANT 891
@891
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 892
@892
D=A
@0
A=M
M=D
@0
M=M+1
// lt
@RETURN_LT_10
D=A
@R13
M=D
@LT
D=A
0;JMP
(RETURN_LT_10)
@0
M=M+1
// push CONSTANT 891
@891
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 891
@891
D=A
@0
A=M
M=D
@0
M=M+1
// lt
@RETURN_LT_12
D=A
@R13
M=D
@LT
D=A
0;JMP
(RETURN_LT_12)
@0
M=M+1
// push CONSTANT 32767
@32767
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 32766
@32766
D=A
@0
A=M
M=D
@0
M=M+1
// gt
@RETURN_GT_14
D=A
@R13
M=D
@GT
D=A
0;JMP
(RETURN_GT_14)
@0
M=M+1
// push CONSTANT 32766
@32766
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 32767
@32767
D=A
@0
A=M
M=D
@0
M=M+1
// gt
@RETURN_GT_16
D=A
@R13
M=D
@GT
D=A
0;JMP
(RETURN_GT_16)
@0
M=M+1
// push CONSTANT 32766
@32766
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 32766
@32766
D=A
@0
A=M
M=D
@0
M=M+1
// gt
@RETURN_GT_18
D=A
@R13
M=D
@GT
D=A
0;JMP
(RETURN_GT_18)
@0
M=M+1
// push CONSTANT 57
@57
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 31
@31
D=A
@0
A=M
M=D
@0
M=M+1
// push CONSTANT 53
@53
D=A
@0
A=M
M=D
@0
M=M+1
// add
@RETURN_ADD_21
D=A
@R13
M=D
@ADD
D=A
0;JMP
(RETURN_ADD_21)
@0
M=M+1
// push CONSTANT 112
@112
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
// and
@RETURN_AND_22
D=A
@R13
M=D
@AND
D=A
0;JMP
(RETURN_AND_22)
@0
M=M+1
// push CONSTANT 82
@82
D=A
@0
A=M
M=D
@0
M=M+1
// or
@RETURN_OR_23
D=A
@R13
M=D
@OR
D=A
0;JMP
(RETURN_OR_23)
@0
M=M+1
// not
@RETURN_NOT_23
D=A
@R13
M=D
@NOT
D=A
0;JMP
(RETURN_NOT_23)
@0
M=M+1
(END)
@END
0;JMP
(EQ)
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
@TRUE
D;JEQ
@FALSE
0;JMP
(LT)
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
@TRUE
D;JLT
@FALSE
0;JMP
(GT)
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
@TRUE
D;JGT
@FALSE
0;JMP
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
@R13
A=M
D=M
0;JMP
(AND)
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
D=D&M
@0
M=M-1
@0
A=M
M=D
@R13
A=M
D=M
0;JMP
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
@R13
A=M
D=M
0;JMP
(NOT)
@0
M=M-1
@0
A=M
D=M
D=!D
@0
A=M
M=D
@R13
A=M
D=M
0;JMP
(TRUE)
@0
A=M
D=-1
M=D
@R13
A=M
D=M
0;JMP
(FALSE)
@0
A=M
D=0
M=D
@R13
A=M
D=M
0;JMP