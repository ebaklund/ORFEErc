using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public record Result
{
    internal Result()
    {
    }

    //

    public static Result Ok()
    {
        return new Ok();
    }

    //

    public static Result Error(string message)
    {
        return new Error(message);
    }

    public static Result Error(string message, Exception innerEx)
    {
        return new Error(message, innerEx);
    }

    //

    public static Result<T> Ok<T>(T value)
    {
        return new Ok<T>(value);
    }

    //

    public static Result<T> Nothing<T>(string message)
    {
        return new Nothing<T>(message);
    }

    public static Result<T> Nothing<T>(string message, Exception innerEx)
    {
        return new Nothing<T>(message, innerEx);
    }

    //

    public static Result<T> Error<T>(string message)
    {
        return new Error<T>(message);
    }

    public static Result<T> Error<T>(string message, Exception innerEx)
    {
        return new Error<T>(message, innerEx);
    }
}
