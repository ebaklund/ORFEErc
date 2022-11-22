
namespace Outcome;

public record Ok<T>(T Value) : Outcome<T>;

public static class OkTOutcomeExtension
{
    public static Ok<T> OkOrThrow<T>(this Outcome<T> result)
    {
        return result switch
        {
            Ok<T> ok => ok,
            _ => throw new System.InvalidCastException($"Input Outcome is not of type: Ok<{typeof(T).Name}>.")
        };
    }

    public static T ValueOrThrow<T>(this Outcome<T> result)
    {
        return result switch
        {
            Ok<T> ok => ok.Value,
            _ => throw new System.InvalidCastException($"Input Outcome is not of type: Ok<{typeof(T).Name}>.")
        };
    }
}
