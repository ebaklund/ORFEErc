using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

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

public static class ErrorTOutcomeExtension
{
    public static Error<T> ErrorOrThrow<T>(this Outcome<T> result)
    {
        return result switch
        {
            Error<T> error => error,
            _ => throw new  System.InvalidCastException($"Input Outcome is not of type: Error<{typeof(T).Name}>.")
        };
    }
}
