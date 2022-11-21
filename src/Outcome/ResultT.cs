
namespace Outcome;

public record Result<T>
{
    public static Result<T> Ok(T value)
    {
        return new Ok<T>(value);
    }

    public static Result<T> Nil(string message)
    {
        return new Nil<T>(message);
    }

    public static Result<T> Nil(string message, Exception innerEx)
    {
        return new Nil<T>(message, innerEx);
    }

    public static Result<T> Error(string message)
    {
        return new Error<T>(message);
    }

    public static Result<T> Error(string message, Exception innerEx)
    {
        return new Error<T>(message, innerEx);
    }
}
