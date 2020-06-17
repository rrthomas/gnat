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
1 MPUSHI MPOP
END-PRIMITIVE

1 1 PRIMITIVE PICK
4 MPUSHI MMUL 1 MPUSHI MDUP \ FIXME: Target cell, not 4!
MADD MLOAD
END-PRIMITIVE

2 2 PRIMITIVE SWAP
0 MPUSHI MSWAP
END-PRIMITIVE

0 1 PRIMITIVE CELL
4 MPUSHI
END-PRIMITIVE

0 1 PRIMITIVE -CELL
-4 MPUSHI
END-PRIMITIVE


\ Memory primitives

1 1 PRIMITIVE @
MLOAD
END-PRIMITIVE

2 0 PRIMITIVE !
MSTORE
END-PRIMITIVE

1 1 PRIMITIVE C@
MLOAD1
END-PRIMITIVE

2 0 PRIMITIVE C!
MSTORE1
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

2 2 PRIMITIVE U/MOD
MUDIVMOD 0 MPUSHI MSWAP
END-PRIMITIVE

2 2 PRIMITIVE S/REM
0 MPUSHI MDUP MNOT 1 MPUSHI
MULT 20 ( FIXME: 5 TARGET-CELLS ) MPUSHRELI MJUMPZ MEXTRA
1 MPUSHI MDUP MPUSH MXOR
1 31 LSHIFT , \ FIXME: constant!
1 MPUSHI MULT 8 ( FIXME: 2 TARGET-CELLS ) MPUSHRELI MJUMPZ
MNOT 0 MPUSHI MSWAP 8 ( FIXME: 2 TARGET-CELLS ) MPUSHRELI
MJUMP MEXTRA MEXTRA MEXTRA
MDIVMOD 0 MPUSHI MSWAP
END-PRIMITIVE

2 1 PRIMITIVE =
MXOR 1 MPUSHI MULT MNEGATE
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

1 0 PRIMITIVE HALT
MHALT
END-PRIMITIVE

2 0 PRIMITIVE MIT_SET_PC
MSET_PC
END-PRIMITIVE

2 0 PRIMITIVE MIT_SET_IR
MSET_IR
END-PRIMITIVE

2 0 PRIMITIVE MIT_SET_STACK
MSET_STACK
END-PRIMITIVE

2 0 PRIMITIVE MIT_SET_STACK_WORDS
MSET_STACK_WORDS
END-PRIMITIVE

2 1 PRIMITIVE MIT_PUSH_STACK
MPUSH_STACK
END-PRIMITIVE

0 1 PRIMITIVE MIT_SIZEOF_STATE
MSIZEOF_STATE
END-PRIMITIVE

1 1 PRIMITIVE MIT_RUN
MRUN
END-PRIMITIVE


\ Control primitives

0 1 PRIMITIVE (LITERAL)
END-PRIMITIVE
0 INLINE \ Prevent inlining, too large (with stack manipulation)
COMPILING

\ Not (quite) a primitive because it is not called like one
CODE (CREATE)
1 MPUSHI MPOP \ discard return address
\ FIXME: 1 COMPILE->S, but with two items on stack on top of SP
2 MPUSHI MDUP -4 MPUSHI MADD \ FIXME: target CELL, not 4
2 MPUSHI MSWAP -4 MPUSHI MADD \ FIXME: target CELL, not 4
MSTORE MJUMP
END-CODE

INCLUDE" primitive-bracket-does.fs"


\ Stack management

VARIABLE RP
\ FIXME: LINK and UNLINK repeat >R and R>
\ FIXME: Allow to be inlined (currently cannot because of offset to RP)
CODE LINK
0 MPUSHI MSWAP MPUSHREL 0 MPUSHI
' RP >BODY OFFSET,
MDUP MLOAD -4 MPUSHI MADD \ FIXME: target -CELL, not -4
0 MPUSHI MDUP 1 MPUSHI MSWAP
MSTORE MSTORE MJUMP
END-CODE

CODE UNLINK
1 MPUSHI MPOP MPUSHREL 0 MPUSHI
' RP >BODY OFFSET,
MDUP MLOAD 0 MPUSHI MDUP
4 MPUSHI MADD 0 MPUSHI MSWAP \ FIXME: target CELL, not 4
1 MPUSHI MSWAP MSTORE MLOAD
MJUMP
END-CODE

1 0 PRIMITIVE >R
MPUSHREL 0 MPUSHI MDUP MLOAD
' RP >BODY OFFSET,
-4 MPUSHI MADD 0 MPUSHI MDUP \ FIXME: target -CELL, not -4
1 MPUSHI MSWAP MSTORE MSTORE
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the start of each word!

0 1 PRIMITIVE R>
MPUSHREL 0 MPUSHI MDUP MLOAD
' RP >BODY OFFSET,
0 MPUSHI MDUP 4 MPUSHI MADD \ FIXME: target CELL, not 4
0 MPUSHI MSWAP 1 MPUSHI MSWAP
MSTORE MLOAD
END-PRIMITIVE
0 INLINE \ Prevent inlining: it's too long to go at the end of each word!

0 1 PRIMITIVE R@
MPUSHREL MLOAD MLOAD
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

0 1 PRIMITIVE RP@
MPUSHREL MLOAD
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

\ FIXME: -9 THROW if RP is out of range
\ Must be a primitive as it would mess up its own return
1 0 PRIMITIVE RP!
MPUSHREL MSTORE NOPALIGN
' RP >BODY OFFSET,
END-PRIMITIVE
0 INLINE \ Prevent inlining because of relative offset to RP

0 1 PRIMITIVE TOTAL-ARGS
MARGC
END-PRIMITIVE

0 1 PRIMITIVE ARGV
MARGV
END-PRIMITIVE


\ Stack management primitives

\ SP is on top of stack on entry to a primitive
0 1 PRIMITIVE SP@
0 MPUSHI MDUP
END-PRIMITIVE

1 0 PRIMITIVE SP!
MPUSHREL MSTORE
0 MPUSHI MSWAP 1 MPUSHI MPOP
END-PRIMITIVE
