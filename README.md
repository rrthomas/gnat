# Gnat

https://github.com/rrthomas/gnat  

by Reuben Thomas <rrt@sc3d.org>  

Gnat is an experimental language. It is released purely in the hope that it
might be interesting or useful to someone.

Gnat comes pre-compiled for the [Bee VM](https://github.com/rrthomas/bee)
(`src/bee/gnat-32.bin` and `src/bee/gnat-64.bin`).


## Copyright and Disclaimer

The package is distributed under the GNU Public License version 3, or, at
your option, any later version. See the file COPYING.

THIS PROGRAM IS PROVIDED AS IS, WITH NO WARRANTY. USE IS AT THE USER'S RISK.


## Installation

Bee is required (see above) in either a 32- or 64-bit build.

### Building from a release tarball

From an unpacked release tarball, run:

```
./configure && make && make check && [sudo] make install
```

See the file `INSTALL` or the output of `./configure --help` for more
information.

### Building from git

To build from a git checkout, GNU autotools (autoconf and automake) are also
required. Run:

```
autoreconf -fi
```

and then proceed as above for a release build.


## Bugs and comments

Please file bug reports and make comments on
[GitHub](https://github.com/rrthomas/gnat/issues), or by email (see
above).
