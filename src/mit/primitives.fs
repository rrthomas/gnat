\ (c) Reuben Thomas 2018-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Required primitives )

INCLUDE" call-cells.fs" CELLS >'FORTH TO PRIMITIVE-RP


\ Stack primitives

1 0 PRIMITIVE DROP
MLIT_1 MPOP
END-PRIMITIVE

1 1 PRIMITIVE PICK
MDUP
END-PRIMITIVE

2 2 PRIMITIVE SWAP
MLIT_0 MSWAP
END-PRIMITIVE

0 1 PRIMITIVE CELL
MLIT NOPALIGN 4 ,
END-PRIMITIVE

0 1 PRIMITIVE -CELL
MLIT NOPALIGN -4 ,
END-PRIMITIVE


\ Memory primitives

1 1 PRIMITIVE @
MLIT_2 MLOAD \ FIXME: constant!
END-PRIMITIVE

2 0 PRIMITIVE !
MLIT_2 MSTORE
END-PRIMITIVE

1 1 PRIMITIVE C@
MLIT_0 MLOAD
END-PRIMITIVE

2 0 PRIMITIVE C!
MLIT_0 MSTORE
END-PRIMITIVE


\ Arithmetic and logical primitives

2 1 PRIMITIVE +
MADD
END-PRIMITIVE

1 1 PRIMITIVE NEGATE
MNEGATE
END-PRIMITIVE

2 1 PRIMITIVE *
MMUL
END-PRIMITIVE

\ FIXME: check for division by zero
2 2 PRIMITIVE U/MOD
MUDIVMOD
MLIT_0 MSWAP
END-PRIMITIVE

2 2 PRIMITIVE S/REM
MDIVMOD
MLIT_0 MSWAP
END-PRIMITIVE

2 1 PRIMITIVE =
MXOR MLIT_1 MULT
MNEGATE
END-PRIMITIVE

2 1 PRIMITIVE <
MLT
MNEGATE
END-PRIMITIVE

2 1 PRIMITIVE U<
MULT
MNEGATE
END-PRIMITIVE

2 1 PRIMITIVE AND
MAND
END-PRIMITIVE

2 1 PRIMITIVE OR
MOR
END-PRIMITIVE

2 1 PRIMITIVE XOR
MXOR
END-PRIMITIVE

1 1 PRIMITIVE INVERT
MNOT
END-PRIMITIVE

2 1 PRIMITIVE LSHIFT
MLSHIFT
END-PRIMITIVE

2 1 PRIMITIVE RSHIFT
MRSHIFT
END-PRIMITIVE


\ System primitives

0 LIBC-PRIMITIVE HALT


\ Control primitives

INCLUDE" primitive-bracket-does.fs"


\ Stack management

VARIABLE RP
\ FIXME: >R and R> must be defined as CODE words, because they are needed by
\ LINK, and UNLINK,
1 0 PRIMITIVE >R
MLIT_PC_REL MLIT_0 MDUP MLIT_2 \ FIXME: constant!
' RP >BODY OFFSET,
MLOAD MLIT MADD MLIT_0
-4 , \ FIXME: target -CELL, not -4
MDUP MLIT_1 MSWAP MLIT_2 \ FIXME: constant!
MSTORE MLIT_2 MSTORE \ FIXME: constant!
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R>
MLIT_PC_REL MLIT_0 MDUP MLIT_2 \ FIXME: constant!
' RP >BODY OFFSET,
MLOAD MLIT_0 MDUP MLIT
4 , \ FIXME: target CELL, not 4
MADD MLIT_0 MSWAP MLIT_1
MSWAP MLIT_2 MSTORE MLIT_2 \ FIXME: constant! x 2
MLOAD
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R@
MLIT_PC_REL MLIT_2 MLOAD MLIT_2 \ FIXME: constant! × 2
' RP >BODY OFFSET,
MLOAD
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

0 1 PRIMITIVE RP@
MLIT_PC_REL MLIT_2 MLOAD NOPALIGN \ FIXME: constant!
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

\ FIXME: -9 THROW if RP is out of range
\ Must be a primitive as it would mess up its own return
1 0 PRIMITIVE RP!
MLIT_PC_REL MLIT_2 MSTORE NOPALIGN \ FIXME: constant!
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

0 LIBMITFEATURES-PRIMITIVE TOTAL-ARGS
1 LIBMITFEATURES-PRIMITIVE MIT_ARGV
2 LIBMITFEATURES-PRIMITIVE MIT_EXTRA_INSTRUCTION


\ Stack management primitives

0 0 PRIMITIVE SP@
MCURRENT_STATE MGET_STACK_DEPTH MLIT_2 MLSHIFT \ FIXME constant!
END-PRIMITIVE

\ FIXME: -9 THROW if out of range
1 0 PRIMITIVE SP!
MLIT_2 MRSHIFT MCURRENT_STATE MSET_STACK_DEPTH \ FIXME constant!
END-PRIMITIVE
