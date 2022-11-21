using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public record Nil<T> : Undefined<T>
{
    internal Nil(string message)
        : base(new NilResultException(message))
    {
    }

    internal Nil(string message, Exception innerEx)
        : base(new NilResultException(message, innerEx))
    {
    }
}

public static class NilExtension
{
    public static Nil<T> NilOrThrow<T>(this Result<T> result)
    {
        return result switch
        {
            Nil<T> nothing => nothing,
            _ => throw new System.InvalidCastException($"Input Result is not of type: Nil<{typeof(T).Name}>.")
        };
    }
}