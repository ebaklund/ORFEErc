
using System.Security.Cryptography;
using System;

namespace OutcomeCs;

public record Outcome<T> : CheckCountedOutcome
{
    public static Outcome<T> Ok(T value)
    {
        return new Ok<T>(value);
    }

    public static Outcome<T> Nil(string message)
    {
        return new Nil<T>(message);
    }

    public static Outcome<T> Nil(string message, Exception innerEx)
    {
        return new Nil<T>(message, innerEx);
    }

    public static Outcome<T> Error(string message)
    {
        return new Error<T>(message);
    }

    public static Outcome<T> Error(string message, Exception innerEx)
    {
        return new Error<T>(message, innerEx);
    }

    public static Outcome<T> FromRunning(Func<T> func)
    {
        try
        {
            T value = func();

            return value switch
            {
                null => Outcome<T>.Nil($"Outcome<{typeof(T).Name}> wrapper received a null value."),
                _ => Outcome<T>.Ok(value)
            };
        }
        catch (Exception innerEx)
        {
            return Outcome<T>.Error($"Outcome<{typeof(T).Name}> wrapper received an exception.", innerEx);
        }
    }
}
