\ FIXME: common up with top-level does.fs

: DOES>   NOPALIGN  POSTPONE (DOES>)  NOPALIGN  HERE CELL+  LAST  2DUP - CELL/  SWAP >INFO
   DUP @ ROT OR  SWAP !  <'FORTH ,  POSTPONE (DOES) LINK, ; IMMEDIATE COMPILING