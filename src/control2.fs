\ (c) Reuben Thomas 2016-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Control structures #2

: BEGIN   NOPALIGN HERE ; IMMEDIATE COMPILING
: AGAIN   POSTPONE AHEAD  SWAP JOIN ; IMMEDIATE COMPILING
: UNTIL   POSTPONE IF  SWAP JOIN ; IMMEDIATE COMPILING
: THEN   NOPALIGN HERE JOIN ; IMMEDIATE COMPILING

: CS-PICK   PICK ; COMPILING
: CS-ROLL   ROLL ; COMPILING

: WHILE   POSTPONE IF  1 CS-ROLL ; IMMEDIATE COMPILING
: REPEAT   POSTPONE AGAIN  POSTPONE THEN ; IMMEDIATE COMPILING
: ELSE   POSTPONE AHEAD  1 CS-ROLL  POSTPONE THEN ; IMMEDIATE COMPILING

VARIABLE 'NODE
: >NODE   'NODE @  BEGIN  ?DUP WHILE  DUP @BRANCH  SWAP HERE  JOIN  REPEAT ;
: I   POSTPONE R@ ; IMMEDIATE COMPILING
: LEAVE   LEAVE,  POSTPONE AHEAD  'NODE  2DUP @  JOIN  ! ; IMMEDIATE COMPILING
: DO   'NODE @  0 'NODE !  DO,  POSTPONE BEGIN ; IMMEDIATE COMPILING
: ?DO   'NODE @  0 'NODE !  POSTPONE 2DUP DO,  POSTPONE = POSTPONE IF
   POSTPONE LEAVE POSTPONE THEN  POSTPONE BEGIN ; IMMEDIATE COMPILING
: LOOP   LOOP,  >NODE  'NODE !  END-LOOP, ; IMMEDIATE COMPILING
: +LOOP   +LOOP,  >NODE  'NODE !  END-LOOP, ; IMMEDIATE COMPILING

: RECURSE   LAST COMPILE, ; IMMEDIATE COMPILING

: CASE   0 ; IMMEDIATE COMPILING
: OF   1+ >R  POSTPONE OVER POSTPONE = POSTPONE IF  POSTPONE DROP  R> ;
IMMEDIATE COMPILING
: ENDOF   >R  POSTPONE ELSE  R> ; IMMEDIATE COMPILING
: ENDCASE   POSTPONE DROP  0 ?DO  POSTPONE THEN  LOOP ; IMMEDIATE COMPILING
