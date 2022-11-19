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

# Ontology

The ontology of the classes defined in this library is based on a classification of computational results.

```
[Result]
```

The computations may succeed or fail.

```
     [Result]
      /    \
[Success]  [Failure]   
```
At the same time, the computation may have yielded a value (or resource). It is still a computational result, but yields additional information of type **T**.
```
           [Result]
         /     |     \
[Success]  [Failure]  [Resource<T>] 
```
The computation yielding a value may also have succeeded or failed.
```
           [Result]
         /     |     \
[Success]  [Failure]  [Resource<T>] 
                         /    \
               [Success<T>]  [Failure<T>]              
```
Although in in this case we are usually more interested in the value yielded and not in the computation itself.
We can think of this that as that the value is a resource, input for further computation.
The value will either be defined or undefined. We rename accordingly.
```
           [Result]
         /     |     \
[Success]  [Failure]  [Resource<T>] 
                        /    \
              [Defined<T>]  [Undefined<T>]              
```
There are several reasons why a function cannot provide a defined resource, but they generally fall in two categories:
1. The preconditions for a function to work (domain) are not well defined. We call this an error.
2. The preconditions are valid, but even so, the value or resource cannot be produced. We represent this with the exceptional value Nil.
```
           [Result]
         /     |     \
[Success]  [Failure]  [Resource<T>] 
                        /    \
              [Defined<T>]  [Undefined<T>]              
                               /    \
                         [Nil<T>]  [Error<T>]              
```
For example, let us assume that we have a function that returns the first element in an array.
It would be resonable to make the following computational rules:
- If the input array do not exist (is null), then we return an error.
- If the input array do exist but it is empty, that is ok, but since there is no value to return we choose to return the value Nil.
- Otherwise, we just return the first element.
```
  Result First(List<T> arr)
  {
     if (arr is null)
       return Result.Error<T>("Array is undefined");
       
     if (arr.Length == 0)
       return Result.Nil<T>("Array is empty");
       
     return arr[0];
  }
```
Of cource there are other situations where we would consider it an error if we cannot produce the first element.
That leads to a different choice of results.
```
  Result First(List<T> arr)
  {
     if (arr is null || )
       return Result.Error<T>("Array is undefined");
       
     if (arr.Length == 0)
       return Result.Error<T>("Array is empty");
       
     return arr[0];
  }
```
The basic ontology is now in place.
However, it is possible to do some syntactical changes that arguably can make this library more intuitive to use.
This can be done by first extending the ontology with some aliases. Aliases do not add semantics to the ontology, they just refer to parent consepts with a different name. Alias relations are indicated with the notation `||`.
```
           [Result]
         /     |     \
[Success]  [Failure]  [Resource<T>] 
   ||         ||           ||
  [Ok]      [Error]    [Result<T>] 
                         /    \
               [Defined<T>]  [Undefined<T>]              
                    ||           /    \
                  [Ok<T>]  [Nil<T>]  [Error<T>]              
```
Preening the ontology by removing the original concept terms, we end up with:
```
           [Result]
         /     |     \
       [Ok] [Error] [Result<T>] 
                      /    \
                 [Ok<T>]  [Undefined<T>]              
                             /    \
                       [Nil<T>]  [Error<T>]              
```
That is the class structure used in this library.
---
Watch Hoare at 27:40: https://www.youtube.com/watch?v=ybrQvs4x0Ps 
