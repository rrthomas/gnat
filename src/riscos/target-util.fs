\ Convert SWI names to numbers
\ ad-hoc list for metacompiling
: OS#   ( c-addr n -- u )
   OVER C@  [CHAR] X = IF            \ If the string starts with X,
      1 /STRING                      \ strip the X
      $20000                         \ set X bit in SWI number
   ELSE
      0                              \ otherwise, clear X bit
   THEN
   -ROT                              \ save SWI number
   "CASE
      S" OS_WriteC"               "OF   $0  "ENDOF
      S" OS_NewLine"              "OF   $3  "ENDOF
      S" OS_ReadC"                "OF   $4  "ENDOF
      S" OS_CLI"                  "OF   $5  "ENDOF
      S" OS_Byte"                 "OF   $6  "ENDOF
      S" OS_File"                 "OF   $8  "ENDOF
      S" OS_Args"                 "OF   $9  "ENDOF
      S" OS_GBPB"                 "OF   $C  "ENDOF
      S" OS_Find"                 "OF   $D  "ENDOF
      S" OS_ReadLine"             "OF   $E  "ENDOF
      S" OS_GetEnv"               "OF  $10  "ENDOF
      S" OS_Exit"                 "OF  $11  "ENDOF
      S" OS_FSControl"            "OF  $29  "ENDOF
      S" OS_SWINumberFromString"  "OF  $39  "ENDOF
      S" OS_SynchroniseCodeAreas" "OF  $6E  "ENDOF
      ." Unknown SWI " TYPE ABORT
   "ENDCASE
   OR ;                              \ or with X bit

: OS   ( regs-in regs-out swi -- )
   -ROT 2DUP + >R ROT
   R@ IF  $E52CB004 CODE,  THEN
   ROT ?DUP IF
      1 SWAP LSHIFT 1- $E8BC0000 OR CODE,
   THEN
   $EF000000 OR CODE,
   ?DUP IF
      1 SWAP LSHIFT 1- $E92C0000 OR CODE,
   THEN
   R> IF  $E49CB004 CODE,  THEN ;
IMMEDIATE COMPILING
: OS"   ( name )   ( regs-in regs-out -- )   [CHAR] " PARSE OS#
   POSTPONE OS ; IMMEDIATE COMPILING

\ FIXME: "os-primitives" does not really describe the following
\ STUB FOO creates an empty word if FOO doesn't exist.
\ This is used to POSTPONE target words that don't exist
\ on the host.
: STUB
   BL WORD \ FIND  0= IF
      HEADER  0 ,                \ reserve enough space for redefinition
                                 \ FIXME: make portable; see REDEFINER
\   ELSE
\      DROP                       \ if found, discard xt
 (  THEN) ;

\ Create stubs for words that may not exist on host
STUB (LITERAL)
STUB (LOOP)
STUB (+LOOP)
STUB UNLOOP
STUB (CREATE)
