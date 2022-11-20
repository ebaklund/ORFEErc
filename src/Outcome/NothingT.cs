using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public record Nothing<T> : Undefined<T>
{
    internal Nothing(string message)
        : base(new NothingResultException(message))
    {
    }

    internal Nothing(string message, Exception innerEx)
        : base(new NothingResultException(message, innerEx))
    {
    }
}

public static class NothingExtension
{
    public static Nothing<T> NothingOrThrow<T>(this Result<T> result)
    {
        return result switch
        {
            Nothing<T> nothing => nothing,
            _ => throw new System.InvalidCastException($"Input Result is not of type: Nothing<{typeof(T).Name}>.")
        };
    }
}