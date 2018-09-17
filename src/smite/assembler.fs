\ SMite assembler for pForth
\ Reuben Thomas   started 1/95

\ FIXME: Add an IP which allows advantage to be taken of packing opcodes
\ into the current word after an instruction with an operand is assembled

CR .( SMite assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS
: BITS   S" ADDRESS-UNIT-BITS" ENVIRONMENT?
   INVERT ABORT" ADDRESS-UNIT-BITS query not supported" ;
BITS  CONSTANT BITS/

FORTH DEFINITIONS
: CODE   BL WORD HEADER  ALSO ASSEMBLER ;
: END-CODE   ALIGN  PREVIOUS ;
ASSEMBLER DEFINITIONS

: INLINE   ( char -- )   LAST >INFO 2 + C! ;

: FIT,   ( x -- )   HERE DUP ALIGNED >-<  0 ?DO  DUP C,
   BITS/ RSHIFT  LOOP  DROP ;
: NOPALIGN   0 FIT, ;

: OPLESS   CREATE C,  DOES> C@ C, ;
: OPFUL   CREATE C,  DOES> C@  C,  NOPALIGN  , ;

: 0OPS   SWAP 1+ SWAP DO  I OPLESS  LOOP ;

$07 $00 0OPS  BNEXT00 BPOP    BPUSH   BSWAP   BRPUSH  BPOP2R  BRPOP   BLT
$0F $08 0OPS  BEQ     BULT    BADD    BMUL    BUDIVMOD BDIVMOD BNEGATE BINVERT
$17 $10 0OPS  BAND    BOR     BXOR    BLSHIFT BRSHIFT BLOAD   BSTORE  BLOADB
$1F $18 0OPS  BSTOREB BPUSH_SP BSTORE_SP BPUSH_RP BSTORE_RP BPUSH_PC BBRANCH BBRANCHZ
$27 $20 0OPS  BPUSH_S0 BPUSH_SSIZE BPUSH_R0 BPUSH_RSIZE BPUSH_HANDLER BSTORE_HANDLER BPUSH_MEMORY BPUSH_BADPC
$2D $28 0OPS  BPUSH_INVALID BCALL BRET   BTHROW  BHALT   BLINK
$2E     OPFUL BLITERAL

$86 $80 0OPS  BARGC  BARG   BSTDIN BSTDOUT BSTDERR BOPEN-FILE BCLOSE-FILE
$8A $87 0OPS  BREAD-FILE BWRITE-FILE BFILE-POSITION BREPOSITION-FILE
$8C $88 0OPS  BFLUSH-FILE BRENAME-FILE BDELETE-FILE BFILE-SIZE BRESIZE-FILE
    $8D OPLESS BFILE-STATUS

PREVIOUS DEFINITIONS
