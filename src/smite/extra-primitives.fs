CR .( Extra primitives )

ALSO ASSEMBLER

\ Stack primitives
CODE DUP   ] 0 [ BPUSH BRET END-CODE  2 INLINE
CODE OVER   ] 1 [ BPUSH BRET END-CODE  2 INLINE

\ System primitives
1 PRIMITIVES CALL_NATIVE

PREVIOUS
