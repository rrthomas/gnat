\ Eratosthenes sieve
ONLY FORTH DEFINITIONS DECIMAL
400000 CONSTANT SIZE
CREATE FLAGS SIZE ALLOT
: DO-PRIME
   FLAGS SIZE 1 FILL
   0  SIZE 0 DO
      FLAGS I + C@ IF
         I 2* 3 +  DUP I +
         BEGIN  DUP SIZE < WHILE
            0 OVER FLAGS + C! OVER +
         REPEAT    DROP DROP 1+
      THEN
   LOOP
   .  ." primes " ;

\ Sieve of Eratosthenes Don Colburn's version
ONLY FORTH DEFINITIONS DECIMAL
: DO-PRIME-HI
   FLAGS SIZE 1 FILL
   0  SIZE 0 DO
      I FLAGS + C@ IF
         I 2* 3 + DUP I + SIZE < IF
            SIZE FLAGS + OVER I + FLAGS + DO
               0 I C! DUP
            +LOOP
         THEN DROP 1+
      THEN
   LOOP
   .  ." primes " ;