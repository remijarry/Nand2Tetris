@is_screen_black


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
  // set the base address of the first screen row
  @SCREEN
  D=A
  @screen_first_pixel
  M=D

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
    D;JGE

    @isScreenBlack
    D=M
    MD=1



  @LOOP
  0;JMP


(PAINT_SCREEN_WHITE)
  // set the base address of the first screen row
  @SCREEN
  D=A
  @screen_first_pixel
  M=D

  // if screen is already white then exit
  @isScreenBlack
  D=M
  @LOOP
  D;JEQ

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
  (WHITE_LOOP)
    // set the current word to black
    @screen_first_pixel
    A=M
    M=0
    @screen_first_pixel
    D=M
    D=D+1
    M=D

    // decrement nbCols
    @nbCol
    D=M
    MD=D-1
    @WHITE_LOOP
    D;JGE

    @isScreenBlack
    D=M
    MD=0

  @LOOP
  0;JMP