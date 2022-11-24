using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutcomeCs;

public record Error<T> : Undefined<T>
{
    internal Error(string message)
        : base(new ErrorOutcomeException(message))
    {
    }

    internal Error(string message, Exception innerEx)
        : base(new ErrorOutcomeException(message, innerEx))
    {
    }
}

public static class ErrorTOutcomeExtensions
{
    public static Error<T> ErrorOrThrow<T>(this Outcome<T> outcome)
    {
        outcome.IncrementCheckCounter();

        return outcome switch
        {
            Error<T> error => error,
            _ => throw new  System.InvalidCastException($"Input Outcome is not of type: Error<{typeof(T).Name}>.")
        };
    }

    public static async Task<Error<T>> ErrorOrThrowAsync<T>(this Task<Outcome<T>> task)
    {
        return (await task.ConfigureAwait(false)).ErrorOrThrow();
    }
}
