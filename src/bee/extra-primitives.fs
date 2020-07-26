\ (c) Reuben Thomas 2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Extra primitives )

ALSO ASSEMBLER


\ Stack primitives

CODE DUP
   0 BPUSHI BDUP
   BRET
END-CODE
2 INLINE

CODE SWAP
   0 BPUSHI BSWAP
   BRET
END-CODE
2 INLINE

CODE OVER
   1 BPUSHI BDUP
   BRET
END-CODE
2 INLINE

CODE ROT
   0 BPUSHI BSWAP
   1 BPUSHI BSWAP
   BRET
END-CODE
4 INLINE

CODE -ROT
   1 BPUSHI BSWAP
   0 BPUSHI BSWAP
   BRET
END-CODE
4 INLINE

CODE 2SWAP
   1 BPUSHI BSWAP
   0 BPUSHI BSWAP
   2 BPUSHI BSWAP
   0 BPUSHI BSWAP
   BRET
END-CODE
8 INLINE


\ Arithmetic and logical primitives

CODE -
   BNEG BADD
   BRET
END-CODE
2 INLINE

CODE 1+
   BNOT BNEG
   BRET
END-CODE
2 INLINE

CODE 1-
   BNEG BNOT
   BRET
END-CODE
2 INLINE

CODE ARSHIFT
   BARSHIFT
   BRET
END-CODE
1 INLINE


PREVIOUS
