CR .( Assembler words )

ALSO ASSEMBLER

CODE DUP   0 B(LITERAL) BPICK BEXIT END-CODE
 2 PRIMITIVES DROP SWAP
CODE OVER   1 B(LITERAL) BPICK BEXIT END-CODE
CODE ROT   2 B(LITERAL) BROLL BEXIT END-CODE
 3 PRIMITIVES PICK ROLL >R
 4 PRIMITIVES R> < = U<
CODE R@   1 B(LITERAL) BRPICK BEXIT END-CODE
: CELL   4 ;
: -CELL   -4 ;
 2 PRIMITIVES + *
CODE -   BNEGATE B+ BEXIT END-CODE  2 INLINE
 4 PRIMITIVES THROW UMOD/ SREM/ NEGATE
CODE U/MOD   BUMOD/ BSWAP BEXIT END-CODE  2 INLINE
CODE S/REM   BSREM/ BSWAP BEXIT END-CODE  2 INLINE
10 PRIMITIVES AND OR XOR INVERT LSHIFT RSHIFT @ ! C@ C!
1 PRIMITIVES EXIT
CODE J   3 B(LITERAL) BRPICK BEXIT END-CODE
INCLUDE" bracket-create.fs"
INCLUDE" bracket-does.fs"
 6 PRIMITIVES SP@ SP! RP@ RP! 'THROW! MEMORY@
CODE S0   BS0@ BEXIT END-CODE  1 INLINE
CODE R0   BR0@ BEXIT END-CODE  1 INLINE
CODE STACK-CELLS   B#S BEXIT END-CODE  1 INLINE
CODE RETURN-STACK-CELLS   B#R BEXIT END-CODE  1 INLINE
 2 PRIMITIVES HALT LINK
CODE TOTAL-ARGS   BARGC BEXIT END-CODE  1 INLINE
CODE ABSOLUTE-ARG   BARG BEXIT END-CODE  1 INLINE
 7 PRIMITIVES STDIN STDOUT STDERR OPEN-FILE CLOSE-FILE READ-FILE WRITE-FILE
 4 PRIMITIVES FILE-POSITION REPOSITION-FILE FLUSH-FILE RENAME-FILE
 4 PRIMITIVES DELETE-FILE FILE-SIZE RESIZE-FILE FILE-STATUS

PREVIOUS