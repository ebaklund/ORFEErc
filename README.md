# The Outcome library (prototype)
Opinionated result handling for c#.

_"... This led me to suggest that the null value is a member of every type, and a null check is required on every use of that reference variable, and it may be perhaps a billion dollar mistake."_ --- Sir Charles Antony Richard Hoare co-inventor of ALGOL 

It is the opinion to the owner of this repo that success handling and failure handling are two sides of the same task.
We will call this task `result handling`.
Unfortunately, C# do not provide the neccesary tools to build robust result handling.
What we are given is `null references` and `exceptions`.

- Both tools can easily be used to signal failed computations.
- But, those signals are easy to overlook by the caller.
- And, none of the tools suggest a consistent way to handle results up the call stack.

The Outcome library attempts to address these issues.
The methods used herein are inspired the Result monads implemented in Haskell, in Rust and in other programming languages.
The monads from different languages work diffenently but all provide some resistance for the caller to use unchecked results.

This library also suggest an idiomatic protocol for handling all outcomes.

# Ontology

The ontology of the classes defined in this library is based on a classification of computational result.

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
[Success]  [Failure]  [Result<T>] 
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
[Success]  [Failure]  [Result<T>] 
                        /    \
              [Defined<T>]  [Undefined<T>]              
```
There are several reasons why a function cannot provide a defined resource, but they generally fall in two categories:
1. The preconditions for a function to work (domain) are not well defined. We call this an error.
2. The preconditions are valid, but even so, the value or resource cannot be produced. We represent this with the exceptional class Nothing.
```
           [Result]
         /     |     \
[Success]  [Failure]  [Result<T>] 
                        /    \
              [Defined<T>]  [Undefined<T>]              
                               /    \
                     [Nothing<T>]  [Error<T>]              
```
For example, let us assume that we have a function that returns the first element in an array.
It would be resonable to make the following computational rules:
- If the input array do not exist (is null), then we return an error.
- If the input array do exist but it is empty, that is ok, but since there is no value to return we choose to return a Nothing object.
- Otherwise, we just return the first element.
```
  Result First(List<T> arr)
  {
     if (arr is null)
       return Result.Error<T>("Array is undefined");
       
     if (arr.Length == 0)
       return Result.Nothing<T>("Array is empty");
       
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
This can be done by first extending the ontology with some aliases. 
Aliases do not add semantics to the ontology, they just refer to parent consepts with a different name. 
Alias relations are indicated with the notation `||`.
```
           [Result]
         /     |     \
[Success]  [Failure]  [Result<T>] 
   ||         ||           |
  [Ok]      [Error]        | 
                         /    \
             [Defined<T>]      [Undefined<T>]              
                  ||               /    \
                [Ok<T>]  [Nothing<T>]  [Error<T>]              
```
Preening the ontology by removing the original concept terms, we end up with:
```
            [Result]
          /     |     \
       [Ok]  [Error]  [Result<T>] 
                        /    \
                   [Ok<T>]  [Undefined<T>]              
                               /    \
                     [Nothing<T>]  [Error<T>]              
```
Even though there is a sematic relation between `Result` and `Result<T>`, we do not actually want to implement it.
We want to let `Result` and `Result<T>` be incompatible in order to have a clear separation between generic and non-generic types.
That makes for more readable code and less confusion for the programmer.

Cutting the relation gives this ontology:
```
           [Result]             [Result<T>]
            /   \                 /    \
         [Ok]  [Error]       [Ok<T>]  [Undefined<T>]              
                                         /    \
                               [Nothing<T>]  [Error<T>]              
```

That is the class structure used in this library.
---
Watch Hoare at 27:40: https://www.youtube.com/watch?v=ybrQvs4x0Ps 
