using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public record Error<T> : Undefined<T>
{
    internal Error(string message)
        : base(new ErrorResultException(message))
    {
    }

    internal Error(string message, Exception innerEx)
        : base(new ErrorResultException(message, innerEx))
    {
    }
}

public static class ErrorTResultExtension
{
    public static Error<T> ErrorOrThrow<T>(this Result<T> result)
    {
        return result switch
        {
            Error<T> error => error,
            _ => throw new  System.InvalidCastException($"Input Result is not of Error<{typeof(T).Name}> type.")
        };
    }
}
