// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/4/Mult.asm

// Multiplies R0 and R1 and stores the result in R2.
// (R0, R1, R2 refer to RAM[0], RAM[1], and RAM[2], respectively.)
// The algorithm is based on repetitive addition.

// set result register to zero.
@R2
M=0

// if R0 = 0 go to end
@R0
D=M
@END
D;JEQ

// if R1 = 0 go to end
@R1
D=M
@END
D;JEQ
D=D-1 // remove 1 so when the loop stops at 0 we have not added one more R1 to the result
M=D

// store the result in R2
@R2
M=0
// Loop R1 times and add R1 to result
(LOOP)
 @R2
 D=M // select R0
 @R0
 D=D+M // add R0 to R0
 @R2
 M=D // update result
 @R1
 D=M
 MD=D-1
 @END
 D;JEQ
 @LOOP
 0;JMP

(END)
  0;JMP