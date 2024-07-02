@is_screen_black

// set the base address of the first screen row
@SCREEN
D=A
@screen_first_pixel
M=D

// main infinite loop
(LOOP)
  // listen to the keyboard
  @KBD
  D=M
  @PAINT_SCREEN_BLACK
  D;JGT
  @PAINT_SCREEN_WHITE
  D;JEQ

  @LOOP
  0;JMP


// first 32 words = first row
// 1 word = 16 bits

(PAINT_SCREEN_BLACK)
  // if screen is already black then exit
  @isScreenBlack
  D=M
  @LOOP
  D;JGT

  // last colum of the screen
  @8191
  D=A
  @nbCol
  M=D

  @R0
  D=A
  @i // i = 0
  M=D

  // while nbCol > 0
  (BLACK_LOOP)
    // set the current word to black
    @screen_first_pixel
    A=M
    M=-1
    @screen_first_pixel
    D=M
    D=D+1
    M=D

    // decrement nbCols
    @nbCol
    D=M
    MD=D-1
    @BLACK_LOOP
    D;JGT


  @LOOP
  0;JMP


(PAINT_SCREEN_WHITE)
  // if screen is already white then exit
  @LOOP
  0;JMP




