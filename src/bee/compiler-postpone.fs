\ (c) Reuben Thomas 2019-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ Compiler words that need special treatment during meta-compilation owing
\ to their use of POSTPONE

\ Compiler
: BRANCH   ( at from to -- )   HERE >R
   ROT DP !  ,  $CF , ( FIXME: BJUMP )  DROP  R> DP ! ;

: AHEAD   HERE  0 ,  $CF , ( FIXME: BJUMP ) ; IMMEDIATE COMPILING
: IF   HERE  0 ,  $DF , ( FIXME: BJUMPZ ) ; IMMEDIATE COMPILING

: DOES-LINK,   POSTPONE R> POSTPONE DROP ;
: LINK, ;
: UNLINK,   $FF , ( FIXME: BRET ) ; COMPILING

: DO,   POSTPONE 2>R ; COMPILING
: LOOP,   POSTPONE (LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: +LOOP,   POSTPONE (+LOOP)  POSTPONE IF  SWAP JOIN ; COMPILING
: END-LOOP,   POSTPONE UNLOOP ; COMPILING

: CREATE,   POSTPONE (CREATE) UNLINK, ;