\ Beetle assembler for pForth
\
\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ FIXME: Add an IP which allows advantage to be taken of packing opcodes
\ into the current word after an instruction with an operand is assembled

CR .( Beetle assembler )


VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS
: BITS   S" ADDRESS-UNIT-BITS" ENVIRONMENT?
   INVERT ABORT" ADDRESS-UNIT-BITS query not supported" ;
BITS  CONSTANT BITS/

FORTH DEFINITIONS
INCLUDE" code.fs"
: END-CODE   ALIGN  PREVIOUS ;
ASSEMBLER DEFINITIONS

: INLINE   ( char -- )   LAST >INFO 2 + C! ;

: FITS   ( x addr -- flag )   DUP ALIGNED >-<  DUP IF  BITS/ * 1-
   1 SWAP LSHIFT  SWAP DUP 0< IF  INVERT  THEN  U>  ELSE NIP  THEN ;
: FIT,   ( x -- )   HERE DUP ALIGNED >-<  0 ?DO  DUP C,
   BITS/ RSHIFT  LOOP  DROP ;
: NOPALIGN   0 FIT, ;

: OPLESS   CREATE C,  DOES> C@ C, ;
: OPFUL   CREATE C,  DOES> C@  OVER HERE 1+ FITS IF  1+ C, FIT,
   ELSE C,  NOPALIGN  ,  THEN ;
: OPADR   CREATE C,  DOES> C@  OVER HERE 1+ ALIGNED - CELL/
   DUP HERE 1+ FITS  IF  SWAP 1+ C, FIT, DROP  ELSE DROP C,
   NOPALIGN  'FORTH - ,  THEN ;

: 0OPS   SWAP 1+ SWAP DO  I OPLESS  LOOP ;
: BOPS   SWAP 1+ SWAP DO  I OPADR  2 +LOOP ;

$07 $00 0OPS  BNEXT00 BDUP    BDROP   BSWAP   BOVER   BROT    B-ROT   BTUCK
$0F $08 0OPS  BNIP    BPICK   BROLL   B?DUP   B>R     BR>     BR@     B<
$17 $10 0OPS  B>      B=      B<>     B0<     B0>     B0=     B0<>    BU<
$1F $18 0OPS  BU>     B0      B1      B-1     BCELL   B-CELL  B+      B-
$27 $20 0OPS  B>-<    B1+     B1-     BCELL+  BCELL-  B*      B/      BMOD
$2F $28 0OPS  B/MOD   BU/MOD  BS/REM  B2/     BCELLS  BABS    BNEGATE BMAX
$37 $30 0OPS  BMIN    BINVERT BAND    BOR     BXOR    BLSHIFT BRSHIFT B1LSHIFT
$3F $38 0OPS  B1RSHIFT B@     B!      BC@     BC!     B+!     BSP@    BSP!
$47 $40 0OPS  BRP@    BRP!    BEP@    BS0@    BS0!    BR0@    BR0!    B'THROW@
$4B $48 0OPS  B'THROW! BMEMORY@ B'BAD@ B-ADDRESS@
$4E $4C BOPS  BBRANCH  B?BRANCH
$51 $50 0OPS  BEXECUTE B@EXECUTE
$52     OPADR BCALL
$55 $54 0OPS  BEXIT   B(DO)
$58 $56 BOPS  B(LOOP) B(+LOOP)
$5B $5A 0OPS  BUNLOOP BJ
$5C     OPFUL B(LITERAL)
$60 $5E 0OPS  BTHROW  BHALT   BLINK

$87 $80 0OPS  BARGC  BARGLEN BARGCOPY BSTDIN-FILENO BSTDOUT-FILENO BSTDERR-FILENO BOPEN-FILE BCLOSE-FILE
$8B $88 0OPS  BREAD-FILE BWRITE-FILE BFILE-POSITION BREPOSITION-FILE
$90 $8C 0OPS  BFLUSH-FILE BRENAME-FILE BDELETE-FILE BFILE-SIZE BRESIZE-FILE
    $91 OPLESS BFILE-STATUS

PREVIOUS DEFINITIONS
