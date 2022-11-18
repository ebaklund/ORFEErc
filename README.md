# ORFEErc (prototype)
Opinionated result handling for c#

_"... This led me to suggest that the null value is a member of every type, and a null check is required on every use of that reference variable, and it may be perhaps a billion dollar mistake."_ --- Sir Charles Antony Richard Hoare co-inventor of ALGOL 

It is the opinion to the owner of this repo that result handling and failure handling are two sides of the same task.
Unfortunately, the tools given in C#, namely null references and exceptions, do not enforce disiplin and protocol neccesary to build robust result handling.

- Both tools can easily be used by a function to signal failed computations.
- But, thos signals are easy to overlook by the caller.
- And, none of the tools suggest a consistent way to handle result and failures up the call stack.

The ORFEE library attempts to address these issues.
The methods used herein are inspired the Result monads implemented in Haskell, Rust and other programming languages.
The monads from different languages work diffenently but all provide some resistance for the caller to use unchecked results.

This library also suggest an idiomatic protocol for handling all results.

---
Watch Hoare at 27:40: https://www.youtube.com/watch?v=ybrQvs4x0Ps 
