\ (c) Reuben Thomas 1995-2019
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

CR .( Required primitives )

\ Stack primitives
3 PRIMITIVES DROP PICK ROLL
3 PRIMITIVES >R R> R@
1 PRIMITIVES CELL

\ Stack management primitives
3 PRIMITIVES RP@ RP! MEMORY@
CODE SP@   BSP@ 2 BLITERAL BLSHIFT BEXIT END-CODE \ FIXME: constant!
CODE SP!   2 BLITERAL BRSHIFT BSP! BEXIT END-CODE \ FIXME: constant!
CODE S0   0 BLITERAL BEXIT END-CODE
CODE R0   0 BLITERAL BEXIT END-CODE

\ Memory primitives
4 PRIMITIVES @ ! C@ C!

\ Arithmetic and logical primitives
3 PRIMITIVES + NEGATE *
CODE (U/MOD)   BU/MOD BEXIT END-CODE 1 INLINE
CODE (S/REM)   BS/REM BEXIT END-CODE 1 INLINE
3 PRIMITIVES = < U<
4 PRIMITIVES AND OR XOR INVERT
2 PRIMITIVES LSHIFT RSHIFT

\ Control primitives
INCLUDE" bracket-does.fs"
2 PRIMITIVES EXIT EXECUTE
CODE @EXECUTE   B@ BEXECUTE BEXIT END-CODE

\ System primitives
3 PRIMITIVES HALT ARGLEN ARGCOPY
CODE TOTAL-ARGS   BARGC BEXIT END-CODE
7 PRIMITIVES STDIN STDOUT STDERR OPEN-FILE CLOSE-FILE READ-FILE WRITE-FILE
2 PRIMITIVES FILE-POSITION REPOSITION-FILE
CODE (CREATE)   BR@ BCELL B+ BEXIT END-CODE