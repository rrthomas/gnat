\ Terminal input/output
\
\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

32 CONSTANT BL
: CR   [ 0 0 ] OS" OS_NewLine" ;
: EMIT   [ 1 0 ] OS" OS_WriteC" ;
: DEL   127 EMIT ;
: PAGE   12 EMIT ;

0 1 PRIMITIVE KEY
SWI," OS_ReadC"
TOP R0 MOV,
CC RET,
R0 126 # MOV,
SWI," XOS_Byte"
END-PRIMITIVE

: DEL?   DUP 127 =  SWAP 8 =  OR ;
: CR?   13 = ;
: EOL   (S")  [ 1 C, 10 C, ALIGN ] ;

: AT-XY   31 EMIT  SWAP EMIT EMIT ;

77 CONSTANT WIDTH   \ width of display