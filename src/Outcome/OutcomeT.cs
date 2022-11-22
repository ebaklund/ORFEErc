
namespace Outcome;

public record Outcome<T>
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
}
