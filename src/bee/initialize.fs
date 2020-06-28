\ (c) Reuben Thomas 1995-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

: INITIALIZE
   \ Assume that we were called by a call instruction at 'FORTH, and
   \ use our return address to calculate the new value of 'FORTH.
   R> CELL- DUP DUP
   [ HERE 0 ,  DUP ] \ address of relocation table
   RELOCATE   \ perform relocations (must be done first)
   TO 'FORTH
   [ BMEMORY@ ]  RETURN-STACK-CELLS CELLS -  SP! \ set SP
   MEMORY@  RETURN-STACK-CELLS CELLS -  \ now start as if stack were empty
   DUP S0!
   START ;
ALIGN  HERE >-<  OP_OFFSET OR  SWAP ! \ FIXME: add !OFFSET
