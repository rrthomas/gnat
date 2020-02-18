\ (c) Reuben Thomas 2018-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler

: R>ADDRESS ; IMMEDIATE COMPILING
: (POSTPONE)   R> R>ADDRESS ALIGNED  DUP CELL+ >R  @ CURRENT-COMPILE, ;

: EXECUTE   STATE @ IF  $03 ,  ELSE  [ $03 , ]  THEN ; IMMEDIATE
\ FIXME: 2 constant!
: @EXECUTE   STATE @ IF  $03080A , 2 ,  ELSE  [ $03080A , 2 , ]  THEN ; IMMEDIATE


\ Data structures

: LITERAL   LITERAL, ; IMMEDIATE COMPILING

: >BODY   2 CELLS + ;
: CREATE,  $01060C0B , CELL , ; \ LIT_PC_REL ( CELL ) LIT_0 SWAP JUMP
: (DOES>)   LAST  DUP  R> R>ADDRESS ALIGNED @  CALL ;