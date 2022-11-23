# Outcome (experimental)

Opinionated outcome handling for c#.

_"... This led me to suggest that the null value is a member of every type, 
and a null check is required on every use of that reference variable, 
and it may be perhaps a billion dollar mistake."_ 
--- Sir Charles Antony Richard Hoare, co-inventor of ALGOL 

_ "Exceptions are worse than gotos (in some ways)"_
--- 
It is the opinion to the owner of this repository that success handling and 
failure handling are two sides of the same task: `outcome handling`.

Unfortunately, C# do not provide the necessary tools to do this task robustly.
We only are given `null references` and `exceptions` as outcome handling tools.
The problem with these tools are that the demand unrealistic acuteness from the programmer and 
do not by themselves suggest a consistent way of handling outcomes.

- Both tools can easily be used to signal failures
- But, those signals are easy to overlook by the caller
- And, none of the tools suggest a consistent way to handle results up the call stack.

The Outcome library attempts to address these issues.
The methods used herein are inspired the Result monads implemented in Haskell, 
Rust and in other programming languages.
The monads from different languages work differently but 
all provide some resistance for the caller to use unchecked results.

This library also suggest an idiomatic protocol for handling all outcomes.

**Outcome is an experimental library where the API can change at any time without notice.**

## Outcome Ontology

The ontology defined in this library is based on a classification of computational outcomes.

```
[Outcome]
```

The computations may succeed or fail.

```
     [Outcome]
      /    \
[Success]  [Failure]   
```
At the same time, the computation may have yielded a value (or resource). It is still a computational result, but yields additional information of some type **T**.
```
           [Outcome]
         /     |     \
[Success]  [Failure]  [Result<T>] 
```
The computation yielding a value may also have succeeded or failed.
```
           [Outcome]
         /     |     \
[Success]  [Failure]  [Resource<T>] 
                         /    \
               [Success<T>]  [Failure<T>]              
```
Although in this case, we are usually more interested in the value yielded and not in the computation itself.
We can think of this that as that the value is a resource, input for further computation.
The value will either be defined or undefined. We rename accordingly.
```
           [Outcome]
         /     |     \
[Success]  [Failure]  [Result<T>] 
                        /    \
              [Defined<T>]  [Undefined<T>]              
```
There are several reasons why a function cannot provide a defined resource, but they generally fall in two categories:
1. The input (domain) and context are not within the valid preconditions for a function to work. We call this an error.
2. The preconditions are valid, but even so, there are situations where it makes sense to return nothing.
```
           [Outcome]
         /     |     \
[Success]  [Failure]  [Result<T>] 
                        /    \
              [Defined<T>]  [Undefined<T>]              
                               /    \
                     [Nothing<T>]  [Error<T>]              
```
For example, let us assume that we have a function that returns the first element in an array.
It would be reasonable to make the following computational rules:
- If the input array do not exist (is `null`), then we return an `Error`.
- If the input array do exist but it is empty, that is OK, but since there is no value to return we choose to return `Nil`.
- If the first element does exist but is `null`, that is OK, but we still return `Nil` to indicate that a real value is not available.
- Otherwise, we just return the first element.
```
    public static Outcome<T> FirstInList_1<T>(List<T> list)
    {
        return list?.Count switch
        {
            null => Outcome<T>.Error("Array is undefined"),
            0 => Outcome<T>.Nothing("Array is empty"),
            _ => list[0] switch
            {
                null => Outcome<T>.Nothing("First item is null"),
                _ => Outcome<T>.Ok(list[0])
            }
        };
    }
```
There are of course other situations where we would consider it an error if the first element does not exists or is  `null`.
That leads to a different choice of outcomes.
```
    public static Outcome<T> FirstInList_2<T>(List<T> list)
    {
        return list?.Count switch
        {
            null => Outcome<T>.Error("Array is undefined"),
            0 => Outcome<T>.Error("Array is empty"), // <== Was Nil
            _ => list[0] switch
            {
                null => Outcome<T>.Error("First item is null"), // <== Was Nil
                _ => Outcome<T>.Ok(list[0])
            }
        };
    }

```
The basic ontology is now in place.
However, it is possible to do some syntactical changes that arguably can make this library more intuitive to use.
This can be done by first extending the ontology with some aliases. 
Aliases do not add semantics to the ontology, they just refer to parent concepts with a different name.
Alias relations are indicated with the notation `||`.
```
           [Outcome]
         /     |     \
[Success]  [Failure]  [Result<T>] 
   ||         ||           |
  [Ok]      [Error]        | 
                         /    \
             [Defined<T>]      [Undefined<T>]              
                  ||               /    \
                [Ok<T>]  [Nothing<T>]  [Error<T>]
                             ||
                           [Nil<T>]              
```
Preening the ontology by removing the original terms, we end up with:
```
            [Outcome]
          /     |     \
       [Ok]  [Error]  [Result<T>] 
                        /    \
                   [Ok<T>]  [Undefined<T>]              
                               /    \
                         [Nil<T>]  [Error<T>]              
```
Even though there is a semantic relation between `Result` and `Result<T>`, we do not actually want to implement it.
We want to let `Result` and `Result<T>` be incompatible in order to have a clear separation between generic and non-generic types.
That makes for more readable code and less confusion for the programmer.

Cutting the relation gives this ontology:
```
          [Outcome]             [Outcome<T>]
            /   \                 /    \
         [Ok]  [Error]       [Ok<T>]  [Undefined<T>]              
                                         /    \
                                   [Nil<T>]  [Error<T>]              
```

That is the class structure used in this library.

## Outcome usage
We will here provide some general advice on which outcome to return in different situations.
We assume that a computational unit either is a subroutine returning `void` or 
a function returning a value of type `T`.

- `Ok` - When a subroutine has successfully completed.
- `Error` - When a subroutine fail to complete it's task. It is often reasonable to map exceptions to `Error`.
- `Ok<T>` - When a function is able to produce a valid value.
- `Nil<T>` - When a function is successful but a return value is not applicable. It is often reasonable to map `null` to `Nil<T>`.
- `Error<T>` - When a function fail. Usually due to invalid input values or unavailable resources.  It is often reasonable to map exceptions to `Error<T>`.

The Outcome library imposes a slight computational overhead.
That should not matter much since the extra drag is diminishing small when it is used for 
higher level outcome handling in the communication between a client and some service. 

The extra drag may be more pronounced if Outcome is used in the inner loops of some RAM-only processes.
It will be up to the programmer to decide if Outcome is a viable tool dependent on circumstance.  

## Outcome example `List<T>`

The .NET generic class `List<T>` is an example of a class throwing exceptions and returning nulls.
In that respect i can serve as a good example of wrap an existing class with Outcome.
The point of this example is not that programmers should start to go around and wrap .NET classes.
The point is to show how some class, using a .NET resource, may expose its result using Outcome.
To avoid an example with distracting code ...

`TODO`


---
Watch Hoare at 27:40 in the video: https://www.youtube.com/watch?v=ybrQvs4x0Ps 
