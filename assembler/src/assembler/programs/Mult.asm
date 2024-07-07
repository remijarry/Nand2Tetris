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


// we copy r0 and r1 somewhere else in the memory, this will serve later to apply the correct sign to the result.
@R0
D=M
@copy_r0
M=D

@R1
D=M
@copy_r1
M=D

//set r0 and r1 to their absolute value
@R0
D=M
@ABS_R0
D;JLT

// we come back to this line after having set up r0 to its absolute value
(NEXT_ABS)
@R1
D=M
@ABS_R1
D;JLT


(MAIN)
// store the result in R2
@R2
M=0 // initialise to 0 (might not be required)
@R1
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
 MD=D-1 // decrement R1.
 @FIX_RESULT_SIGN
 D;JEQ // if we've reached 0, then we are done with the multiplication, we now need to adjust the sign.
 @LOOP
 0;JMP


// if one of the input was negative, change the sign of the result
(FIX_RESULT_SIGN)
  @copy_r0
  D=M
  @R0_IS_NEGATIVE
  D;JLT

  // If (r0 > 0 && r1 < 0) then negate_result
  @copy_r1
  D=M
  @NEGATE_RESULT
  D;JLT

  @END
  0;JMP

(R0_IS_NEGATIVE)
  // if R1 is also negative, result is already positive, we exit
  @copy_r1
  D=M
  @END
  D;JLT
  @NEGATE_RESULT
  D;JGT

(NEGATE_RESULT)
  // we negate the result and exit.
  @R2
  D=M
  D=!D
  D=D+1
  M=D
  @END
  0;JMP

(END)
  @END
  0;JMP


// absolute values
(ABS_R0)
  @R0
  D=M
  D=!D
  D=D+1
  M=D
  @NEXT_ABS
  0;JMP
(ABS_R1)
  @R1
  D=M
  D=!D
  D=D+1
  M=D
  @MAIN
  0;JMP
