
namespace Outcome;

public record Ok<T>(T Value) : Result<T>;

public static class OkTResultExtension
{
    public static Ok<T> OkOrThrow<T>(this Result<T> result)
    {
        return result switch
        {
            Ok<T> ok => ok,
            _ => throw new System.InvalidCastException($"Input Result is not of Ok<{typeof(T).Name}> type.")
        };
    }
}
