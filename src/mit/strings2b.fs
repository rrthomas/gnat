\ (c) Reuben Thomas 1991-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: CLITERAL   $0B , >S, ( FIXME: 5 CELLS MPUSHRELI )  POSTPONE AHEAD -ROT
   ", 0 CALIGN  POSTPONE THEN ; IMMEDIATE COMPILING
: SLITERAL   POSTPONE CLITERAL  POSTPONE COUNT ; IMMEDIATE COMPILING
