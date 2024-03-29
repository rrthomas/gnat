\ Bee assembler for Gnat
\
\ (c) Reuben Thomas 2020-2022
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.


\ VOCABULARY ASSEMBLER  ALSO ASSEMBLER DEFINITIONS

: INLINE   ( char -- )   DUP LAST >NAME .INLINE-COUNT  LAST >INFO 2 + C! ;

\ PREVIOUS DEFINITIONS
: CODE   BL WORD HEADER  ( ALSO ASSEMBLER ) ;
: END-CODE   ALIGN  ( PREVIOUS ) ;
\ ALSO ASSEMBLER DEFINITIONS

INCLUDE" opcodes.fs"

: OPCODE>NAME   ( n -- addr )
   CASE
       0 OF C" nop" ENDOF
       1 OF C" not" ENDOF
       2 OF C" and" ENDOF
       3 OF C" or" ENDOF
       4 OF C" xor" ENDOF
       5 OF C" lshift" ENDOF
       6 OF C" rshift" ENDOF
       7 OF C" arshift" ENDOF
       8 OF C" pop" ENDOF
       9 OF C" dup" ENDOF
      10 OF C" set" ENDOF
      11 OF C" swap" ENDOF
      12 OF C" jump" ENDOF
      13 OF C" jumpz" ENDOF
      14 OF C" call" ENDOF
      15 OF C" ret" ENDOF
      16 OF C" load" ENDOF
      17 OF C" store" ENDOF
      18 OF C" load1" ENDOF
      19 OF C" store1" ENDOF
      20 OF C" load2" ENDOF
      21 OF C" store2" ENDOF
      22 OF C" load4" ENDOF
      23 OF C" store4" ENDOF
      24 OF C" load_ia" ENDOF
      25 OF C" store_db" ENDOF
      26 OF C" load_ib" ENDOF
      27 OF C" store_da" ENDOF
      28 OF C" load_da" ENDOF
      29 OF C" store_ib" ENDOF
      30 OF C" load_db" ENDOF
      31 OF C" store_ia" ENDOF
      32 OF C" neg" ENDOF
      33 OF C" add" ENDOF
      34 OF C" mul" ENDOF
      35 OF C" divmod" ENDOF
      36 OF C" udivmod" ENDOF
      37 OF C" eq" ENDOF
      38 OF C" lt" ENDOF
      39 OF C" ult" ENDOF
      40 OF C" pushs" ENDOF
      41 OF C" pops" ENDOF
      42 OF C" dups" ENDOF
      43 OF C" catch" ENDOF
      44 OF C" throw" ENDOF
      45 OF C" break" ENDOF
      46 OF C" word_bytes" ENDOF
      49 OF C" get_ssize" ENDOF
      50 OF C" get_sp" ENDOF
      51 OF C" set_sp" ENDOF
      52 OF C" get_dsize" ENDOF
      53 OF C" get_dp" ENDOF
      54 OF C" set_dp" ENDOF
      55 OF C" get_handler_sp" ENDOF
      >R  0  R>
   ENDCASE ;


\ Print the disassembly of the given instruction
: DISASSEMBLE   ( pc opcode -- )
   CASE  DUP OPCODE>
      OP_CALLI OF
         OP1_SHIFT ARSHIFT CELLS  + \ compute address
         ." calli " >NAME COUNT TYPE
      ENDOF
      OP_PUSHI OF
         NIP
         OP1_SHIFT ARSHIFT \ compute constant
         ." pushi " DUP . ." # 0x" H.
      ENDOF
      OP_PUSHRELI OF
         OP1_SHIFT ARSHIFT CELLS  + \ compute address
         ." pushreli 0x" H.
      ENDOF
      >R
      CASE  DUP OPCODE2>
         OP2_JUMPI OF
            OP2_SHIFT ARSHIFT CELLS  + \ compute address
            ." jumpi 0x" H.
         ENDOF
         OP2_JUMPZI OF
            OP2_SHIFT ARSHIFT CELLS  + \ compute address
            ." jumpzi 0x" H.
         ENDOF
         OP2_TRAP OF
            NIP
            OP2_SHIFT RSHIFT \ compute trap code
            ." trap 0x" H.
         ENDOF
         OP2_INSN OF
            NIP
            OP2_SHIFT RSHIFT
            #INSTRUCTIONS OVER >  SWAP OPCODE>NAME  TUCK AND IF
               COUNT TYPE
            ELSE
               DROP ." # invalid instruction!"
            THEN
         ENDOF
      ENDCASE
      R>
   ENDCASE
   CR ;

: SHOW   ( a-addr len -- )
   OVER + SWAP DO
      I DUP @ DISASSEMBLE
   CELL +LOOP ;

: TRAP   CREATE OP2_TRAP >OPCODE2 ,  DOES> @  HERE OVER ['] DISASSEMBLE TO-ASMOUT  RAW, ;
: INST   CREATE OP2_INSN >OPCODE2 ,  DOES> @  HERE OVER ['] DISASSEMBLE TO-ASMOUT  RAW, ;
: FOO   CREATE OP2_INSN >OPCODE2 ,  DOES> ? ;
: INSTS   SWAP 1+ SWAP DO  I INST  LOOP ;

: (BCALLI)  ." calli 0x" DUP H. CR  HERE - CELL/ OP_CALLI >OPCODE RAW, ;
: (BPUSHI)   ." pushi 0x" DUP H. CR  OP_PUSHI >OPCODE RAW, ;
: (BPUSHRELI)   ." pushreli 0x" DUP H. CR  HERE SWAP OFFSET OP1_SHIFT ARSHIFT  OP_PUSHRELI >OPCODE , ;
: BCALLI   ['] (BCALLI) TO-ASMOUT ;
: BPUSHI   ['] (BPUSHI) TO-ASMOUT ;
: BPUSHRELI   ['] (BPUSHRELI) TO-ASMOUT ;

 7  0 INSTS BNOP     BNOT     BAND     BOR      BXOR     BLSHIFT  BRSHIFT  BARSHIFT
15  8 INSTS BPOP     BDUP     BSET     BSWAP    BJUMP    BJUMPZ   BCALL    BRET
23 16 INSTS BLOAD    BSTORE   BLOAD1   BSTORE1  BLOAD2   BSTORE2  BLOAD4   BSTORE4
31 24 INSTS BLOAD_IA BSTORE_DB BLOAD_IB BSTORE_DA BLOAD_DA BSTORE_IB BLOAD_DB BSTORE_IA
39 32 INSTS BNEG     BADD     BMUL     BDIVMOD  BUDIVMOD BEQ      BLT      BULT
46 40 INSTS BPUSHR   BPOPR    BDUPR    BCATCH   BTHROW   BBREAK   BWORD_BYTES
55 49 INSTS BGET_SSIZE BGET_SP BSET_SP BGET_DSIZE BGET_DP BSET_DP BGET_HANDLER_SP

0 TRAP LIBC
1 TRAP GLIB

\ PREVIOUS DEFINITIONS
