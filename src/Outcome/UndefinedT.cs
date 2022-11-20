using System;
using System.Collections.Generic;
using System.Linq;

namespace Outcome;

public record Undefined<T>(UndefinedResultException Reason) : Result<T>;

public static class UndefinedTResultExtension
{
    public static Undefined<T> UndefinedOrThrow<T>(this Result<T> result)
    {
        return (result as Undefined<T>) switch
        {
            Undefined<T> undefined => undefined,
            _ => throw new System.InvalidCastException($"Input Result is not of type: Undefined<{typeof(T).Name}>.")
        };
    }
}