using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutcomeCs;

public record Nil<T> : Undefined<T>
{
    internal Nil(string message)
        : base(new NilOutcomeException(message))
    {
    }

    internal Nil(string message, Exception innerEx)
        : base(new NilOutcomeException(message, innerEx))
    {
    }
}

public static class NilExtension
{
    public static Nil<T> NilOrThrow<T>(this Outcome<T> outcome)
    {
        outcome.IncrementCheckCounter();

        return outcome switch
        {
            Nil<T> nothing => nothing,
            _ => throw new System.InvalidCastException($"Input Outcome is not of type: Nil<{typeof(T).Name}>.")
        };
    }
}