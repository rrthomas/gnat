CR .( Required primitives )

ALSO ASSEMBLER \ For INLINE

\ Stack primitives
3 PRIMITIVES DROP PICK ROLL
3 PRIMITIVES >R R> R@

\ Stack management primitives
5 PRIMITIVES SP@ SP! RP@ RP! MEMORY@
CODE S0   BS0@ BEXIT END-CODE  1 INLINE
CODE R0   BR0@ BEXIT END-CODE  1 INLINE
CODE STACK-CELLS   B#S BEXIT END-CODE  1 INLINE
CODE RETURN-STACK-CELLS   B#R BEXIT END-CODE  1 INLINE

\ Memory primitives
4 PRIMITIVES @ ! C@ C!

\ Arithmetic and logical primitives
\ FIXME: make it work without -
4 PRIMITIVES + - NEGATE *
2 PRIMITIVES U/MOD S/REM
3 PRIMITIVES = < U<
4 PRIMITIVES AND OR XOR INVERT
2 PRIMITIVES LSHIFT RSHIFT

\ Control primitives
INCLUDE" bracket-does.fs"

\ System primitives
1 PRIMITIVES HALT
CODE TOTAL-ARGS   BARGC BEXIT END-CODE  1 INLINE
CODE ABSOLUTE-ARG   BARG BEXIT END-CODE  1 INLINE
7 PRIMITIVES STDIN STDOUT STDERR OPEN-FILE CLOSE-FILE READ-FILE WRITE-FILE
2 PRIMITIVES FILE-POSITION REPOSITION-FILE

PREVIOUS