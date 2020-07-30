\ Defining
\
\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: CREATE   BL WORD HEADER  CREATE,  ALIGN ;
INCLUDE" does.fs"

: VARIABLE   CREATE  0 , ( FIXME: CELL ALLOT ) ;
: CONSTANT   BL WORD HEADER  LINK,  POSTPONE LITERAL  UNLINK, ;
: VALUE   CREATE  ,  DOES>  @ ;
: TO   ' >BODY ! ;
   :NONAME   ' >BODY  POSTPONE LITERAL  POSTPONE ! ;IMMEDIATE

: .DEFER-ADDRESS   ." .int " .NAME ." _defer - ." CR ;
: DEFER   CREATE  HERE  ['] ABORT >REL RAW,  LAST >NAME ['] .DEFER-ADDRESS TO-ASMOUT  DOES>  REL@ EXECUTE ;
: ACTION-OF   ' DEFER@ ;
   :NONAME   POSTPONE [']  POSTPONE DEFER@ ;IMMEDIATE
: .DEFER-LABEL   ." .set " .NAME ." _defer, "
   DUP >INFO 3 + C@ IF  >NAME .NAME  ELSE BACKWARD .LABEL  THEN  CR ;
: .DEFER-ABORT   ." .set " .NAME ." _defer, ABORT" CR ;
: IS   '  2DUP >NAME ['] .DEFER-LABEL TO-ASMOUT  DEFER! ;
   :NONAME   POSTPONE [']  POSTPONE DEFER! ;IMMEDIATE
