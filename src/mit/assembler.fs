\ Mit assembler for pForth
\
\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Mit assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS
: BITS   S" ADDRESS-UNIT-BITS" ENVIRONMENT?
   INVERT ABORT" ADDRESS-UNIT-BITS query not supported" ;
BITS  CONSTANT BITS/

VARIABLE PC
VARIABLE I-ADDR
VARIABLE I-SHIFT

ALSO FORTH DEFINITIONS
INCLUDE" code.fs"
: END-CODE   ALIGN  0 I-ADDR !  PREVIOUS ;
PREVIOUS DEFINITIONS

: INLINE   ( char -- )   LAST >INFO 2 + C! ;

8 CONSTANT INSTRUCTION-BIT
1  INSTRUCTION-BIT LSHIFT  1- CONSTANT INSTRUCTION-MASK

: _FETCH   ( -- )   HERE I-ADDR !  0 ,  HERE PC !  0 I-SHIFT ! ;

: INSTRUCTION   ( opcode -- )
   DUP INSTRUCTION-MASK U> ABORT" invalid opcode"
   HERE PC @ <> IF  0 I-ADDR !  THEN    \ invalidate I-ADDR if memory
                                        \ ALLOTted other than by WORD,
   I-ADDR @ 0= IF  _FETCH  THEN         \ start of a new word
   I-ADDR @ @                           \ ( opcode cur-word )
   OVER  I-SHIFT @ LSHIFT  OR           \ ( opcode new-word )
   2DUP I-SHIFT @ RSHIFT <> IF          \ if we ran out of space,
      _FETCH DROP                       \ advance a word
   ELSE
      NIP                               \ otherwise we're fine
   THEN
   I-ADDR @ !                           \ store new-word
   INSTRUCTION-BIT I-SHIFT +! ;

: OPLESS   CREATE C,  DOES> C@ INSTRUCTION ;
: 0OPS   SWAP 1+ SWAP DO  I OPLESS  LOOP ;

$07 $00 0OPS  MEXTRA  MJUMP    MJUMPZ   MCALL   MPOP    MDUP    MSWAP   MTRAP
$0F $08 0OPS  MLOAD   MSTORE   MLOAD1   MSTORE1 MLOAD2  MSTORE2 MLOAD4  MSTORE4
$17 $10 0OPS  MPUSH   MPUSHREL MNOT     MAND    MOR     MXOR    MLT     MULT
$1F $18 0OPS  MLSHIFT MRSHIFT  MARSHIFT MNEGATE MADD    MMUL    MDIVMOD MUDIVMOD

: EXTRA-INSTRUCTION   ( n -- )
   CREATE ,  DOES>  @ NOPALIGN  INSTRUCTION-BIT LSHIFT  , ;

$00 EXTRA-INSTRUCTION MNEXT
$01 EXTRA-INSTRUCTION MHALT
$02 EXTRA-INSTRUCTION MGET_PC
$03 EXTRA-INSTRUCTION MSET_PC
$04 EXTRA-INSTRUCTION MGET_IR
$05 EXTRA-INSTRUCTION MSET_IR
$06 EXTRA-INSTRUCTION MGET_STACK_DEPTH
$07 EXTRA-INSTRUCTION MSET_STACK_DEPTH
$08 EXTRA-INSTRUCTION MGET_STACK
$09 EXTRA-INSTRUCTION MSET_STACK
$0A EXTRA-INSTRUCTION MGET_STACK_WORDS
$0B EXTRA-INSTRUCTION MSET_STACK_WORDS
$0C EXTRA-INSTRUCTION MTHIS_STATE
$0D EXTRA-INSTRUCTION MLOAD_STACK
$0E EXTRA-INSTRUCTION MSTORE_STACK
$0F EXTRA-INSTRUCTION MPOP_STACK
$10 EXTRA-INSTRUCTION MPUSH_STACK
$11 EXTRA-INSTRUCTION MNEW_STATE
$12 EXTRA-INSTRUCTION MFREE_STATE
$13 EXTRA-INSTRUCTION MRUN
$14 EXTRA-INSTRUCTION MSINGLE_STEP
$100 EXTRA-INSTRUCTION MARGC
$101 EXTRA-INSTRUCTION MARGV

0 CONSTANT LIB_C

PREVIOUS DEFINITIONS
