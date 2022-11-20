
namespace Outcome;

public record Ok<T>(T Value) : Result<T>;

public static class OkTResultExtension
{
    public static Ok<T> OkOrThrow<T>(this Result<T> result)
    {
        return result switch
        {
            Ok<T> ok => ok,
            _ => throw new System.InvalidCastException($"Input Result is not of type: Ok<{typeof(T).Name}>.")
        };
    }

    public static T ValueOrThrow<T>(this Result<T> result)
    {
        return result switch
        {
            Ok<T> ok => ok.Value,
            _ => throw new System.InvalidCastException($"Input Result is not of type: Ok<{typeof(T).Name}>.")
        };
    }
}
