# ORFEErc
Opinionated result handling for c#

_"... This led me to suggest that the null value is a member of every type, and a null check is required on every use of that reference variable, and it may be perhaps a billion dollar mistake."_ --- Sir Charles Antony Richard Hoare co-inventor of ALGOL 

It is the opinion to the owner of this repo that result handling and failure handling are two sides of the same task.
Unfortunately, the tools given in C#, namely null references and exceptions, do not enforce disiplin and protocol neccesary to build robust result handling.

- Both tools are easy to use for signaling issues
- But, both tools also produces signals that are easy to overlook
- And, none of the tools suggest a consistent way to chain result and failures up the call hierachy.

The ORFEE library attempts to address these issues.
The methods used herein are inspired the Result monads implemented in Haskell,Rust and other programming languages.
The monads works diffenently but all provide some resistance for the caller to use an unchecked result value.

This library also suggest an idiomatic protocol for handling all function results.

---
Watch Hoare: https://www.youtube.com/watch?v=ybrQvs4x0Ps at 27:40
