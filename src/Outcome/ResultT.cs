
namespace Outcome;

public record Result<T>
{
    public static Result<T> Ok(T value)
    {
        return new Ok<T>(value);
    }

    public static Result<T> Nothing(string message)
    {
        return new Nothing<T>(message);
    }

    public static Result<T> Nothing(string message, Exception innerEx)
    {
        return new Nothing<T>(message, innerEx);
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
