\ Mass storage input/output words
\
\ (c) Reuben Thomas 1996-2020
\
\ The package is distributed under the GNU GPL version 3, or, at your
\ option, any later version.
\
\ THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER’S
\ RISK.

\ ALSO ASSEMBLER

1 1 0 LIBC-PRIMITIVE STRLEN
3 1 1 LIBC-PRIMITIVE STRNCPY
0 1 2 LIBC-PRIMITIVE STDIN-FILENO
0 1 3 LIBC-PRIMITIVE STDOUT-FILENO
0 1 4 LIBC-PRIMITIVE STDERR-FILENO
0 1 5 LIBC-PRIMITIVE R/O
0 1 6 LIBC-PRIMITIVE W/O
0 1 7 LIBC-PRIMITIVE R/W
0 1 8 LIBC-PRIMITIVE O_CREAT
0 1 9 LIBC-PRIMITIVE O_TRUNC
2 1 10 LIBC-PRIMITIVE OPEN
1 1 11 LIBC-PRIMITIVE CLOSE-FILE
3 1 12 LIBC-PRIMITIVE READ
3 1 13 LIBC-PRIMITIVE WRITE
0 1 14 LIBC-PRIMITIVE SEEK_SET
0 1 15 LIBC-PRIMITIVE SEEK_CUR
0 1 16 LIBC-PRIMITIVE SEEK_END
3 1 17 LIBC-PRIMITIVE LSEEK \ FIXME: express off_t more accurately!
1 1 18 LIBC-PRIMITIVE FLUSH-FILE
2 1 19 LIBC-PRIMITIVE RENAME
1 1 20 LIBC-PRIMITIVE REMOVE
1 2 21 LIBC-PRIMITIVE FILE-SIZE \ FIXME: express off_t more accurately!
2 1 22 LIBC-PRIMITIVE RESIZE-FILE \ FIXME: express off_t more accurately!
1 2 23 LIBC-PRIMITIVE FILE-STATUS
0 1 $100 LIBC-PRIMITIVE TOTAL-ARGS
0 1 $101 LIBC-PRIMITIVE ARGV

\ PREVIOUS


: CREATE-FAM   O_CREAT O_TRUNC OR ;

0 CONSTANT BIN-MODE
: BIN ;

: OPEN-FILE   ( c-addr u fam -- fid ior )   -ROT SCRATCH-C0END SWAP OPEN
    DUP 0 < ;
: READ-FILE   ( c-addr u fileid -- nread ior )   READ  DUP 0 < ;
: WRITE-FILE   ( c-addr u fileid -- ior )   WRITE  0 < ;
: RENAME-FILE   ( c-addr1 u1 c-addr2 u2 -- ior )   SCRATCH-C0END -ROT HERE 256 C0END HERE
   SWAP RENAME ;
: DELETE-FILE   ( c-addr u -- ior )   SCRATCH-C0END REMOVE ;
: CREATE-FILE   ( c-addr u fam -- fid ior )   CREATE-FAM OR  OPEN-FILE ;
: FILE-POSITION   0 SEEK_CUR LSEEK  DUP -1 = ;
: REPOSITION-FILE   SWAP  SEEK_SET LSEEK  -1 = ;
: ABSOLUTE-ARG   ( u1 -- c-addr u2 )
   TOTAL-ARGS OVER > IF \ u1
      ARGV  SWAP CELLS + @  DUP STRLEN
   ELSE
      DROP  0 0
   THEN ;