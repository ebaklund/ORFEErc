using System;
using System.Collections.Generic;
using System.Linq;

namespace Outcome;

public record Undefined<T>(UndefinedOutcomeException Reason) : Outcome<T>;

public static class UndefinedTOutcomeExtension
{
    public static Undefined<T> UndefinedOrThrow<T>(this Outcome<T> result)
    {
        return (result as Undefined<T>) switch
        {
            Undefined<T> undefined => undefined,
            _ => throw new System.InvalidCastException($"Input Outcome is not of type: Undefined<{typeof(T).Name}>.")
        };
    }
}