\ Machine-dependent words (Bee)
\
\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

INCLUDE" opcodes.fs"


\ Core compiler

: CALL   ( at from to -- )   >-< SWAP ! ;

: NOP,   INSN_NOP OP2_INSN >OPCODE2 RAW, ;
: CALL,   HERE SWAP OFFSET CELL/ OP_CALLI >OPCODE RAW, ;
: BRANCH,   OP2_JUMPI OP_LEVEL2 >OPCODE RAW, ;
: IF,   OP2_JUMPZI OP_LEVEL2 >OPCODE RAW, ;
: PUSH,   ( x -- )
   DUP CELLS CELL/  OVER = IF
      OP_PUSHI >OPCODE RAW,
   ELSE
      HERE 2 CELLS + CALL,  RAW,  INSN_POPR OP2_INSN >OPCODE2 RAW,  INSN_LOAD OP2_INSN >OPCODE2 RAW,
   THEN ;
: PUSHREL,   HERE SWAP OFFSET CELL/ OP_PUSHRELI >OPCODE RAW, ;

: @BRANCH   ( from -- to )   DUP @ 2 ARSHIFT + $FFFFFFFC AND ;
: !BRANCH   ( from to -- )   OVER - 2 LSHIFT  OVER @ $F AND  OR  SWAP ! ;
: COMPILE,   DUP >INFO 2 + C@  ?DUP IF  0 DO  DUP @ ,  CELL+  LOOP  DROP
   ELSE CALL,  THEN ;

: ADDR>LABEL   'FORTH - CELL/ ;
CHAR b CONSTANT BACKWARD
CHAR f CONSTANT FORWARD
CHAR n CONSTANT NONAME

: BEGIN   HERE  DUP BACKWARD .LABEL-DEF ; IMMEDIATE COMPILING
: AHEAD   HERE  DUP FORWARD .BRANCH  BRANCH, ; IMMEDIATE COMPILING
: IF   HERE  DUP FORWARD .IF  IF, ; IMMEDIATE COMPILING

: THEN   DUP FORWARD .LABEL-DEF  HERE !BRANCH ; IMMEDIATE COMPILING

: LINK, ;
: UNLINK,   .RET  INSN_RET OP2_INSN >OPCODE2 RAW, ; COMPILING
: LEAVE, ;
