CR .( Required primitives )

ALSO ASSEMBLER \ For INLINE

\ Stack primitives

1 0 PRIMITIVE DROP
MPOP
END-PRIMITIVE

1 1 PRIMITIVE PICK
MDUP
END-PRIMITIVE

2 2 PRIMITIVE SWAP
MLIT_0 MSWAP
END-PRIMITIVE

0 1 PRIMITIVE CELL
] 4 [
END-PRIMITIVE

0 1 PRIMITIVE -CELL
] -4 [
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

2 1 PRIMITIVE -
MNEGATE MADD
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

1 0 $2 LIBC-PRIMITIVE HALT


\ Control primitives

\ Must NOT be inline, as it needs caller's PC!
\ FIXME: use LIT_PC_REL
CODE (CREATE)
MLIT_0 MSWAP
MJUMP
END-CODE

INCLUDE" bracket-does.fs"


\ Stack management

\ FIXME: Make this a small constant!
VARIABLE PRIMITIVE-RP

\ FIXME: Make this a small constant!
VARIABLE RP
\ FIXME: >R and R> must be defined as CODE words, because they are needed by
\ LINK, and UNLINK,
1 0 PRIMITIVE >R
' RP >BODY <'FORTH LITERAL,
MLIT_0 MDUP
MLIT_2 MLOAD \ FIXME: constant!
-4 LITERAL, MADD \ FIXME: target -CELL, not -4
MLIT_0 MDUP
MLIT_1 MSWAP
MLIT_2 MSTORE \ FIXME: constant!
MLIT_2 MSTORE \ FIXME: constant!
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R>
' RP >BODY <'FORTH LITERAL,
MLIT_0 MDUP
MLIT_2 MLOAD \ FIXME: constant!
MLIT_0 MDUP
4 LITERAL, MADD \ FIXME: target CELL, not 4
MLIT_0 MSWAP
MLIT_1 MSWAP
MLIT_2 MSTORE \ FIXME: constant!
MLIT_2 MLOAD \ FIXME: constant!
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R@
' RP >BODY <'FORTH LITERAL,
MLIT_2 MLOAD \ FIXME: constant!
MLIT_2 MLOAD \ FIXME: constant!
END-PRIMITIVE

0 1 PRIMITIVE RP@
' RP >BODY <'FORTH LITERAL,
MLIT_2 MLOAD \ FIXME: constant!
END-PRIMITIVE

\ FIXME: -9 THROW if RP is out of range
\ Must be a primitive as it would mess up its own return
1 0 PRIMITIVE RP!
' RP >BODY <'FORTH LITERAL,
MLIT_2 MSTORE \ FIXME: constant!
END-PRIMITIVE


0 NATIVE-POINTER-CELLS $0 LIBMIT-PRIMITIVE MIT_CURRENT_STATE
2 NATIVE-POINTER-CELLS $1 LIBMIT-PRIMITIVE NATIVE_ADDRESS_OF_RANGE

\ Stack management primitives

0 0 PRIMITIVE SP@
MGET_STACK_DEPTH
MLIT_2 MLSHIFT \ FIXME constant!
END-PRIMITIVE

1 0 PRIMITIVE SP!
MLIT_2 MRSHIFT \ FIXME constant!
MSET_STACK_DEPTH
END-PRIMITIVE

\ FIXME: Put in better order; must be defined after bracket-create is included because of use of VALUE
1024 1024 * VALUE MEMORY-SIZE \ FIXME: command-line parameter

\ FIXME: Make these optional in pForth (highlevel.fs does not need them)
4096 CONSTANT STACK-CELLS
4096 CONSTANT RETURN-STACK-CELLS

0 CONSTANT S0
0 VALUE R0


PREVIOUS
