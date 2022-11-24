
namespace OutcomeCs;

public record Ok<T>(T Value) : Outcome<T>;

public static class OkTOutcomeExtensions
{
    public static T ValueOrThrow<T>(this Outcome<T> outcome)
    {
        outcome.IncrementCheckCounter();

        return outcome switch
        {
            Ok<T> ok => ok.Value,
            _ => throw new System.InvalidCastException($"Input Outcome is not of type: Ok<{typeof(T).Name}>.")
        };
    }

    public static async Task<T> ValueOrThrowAsync<T>(this Task<Outcome<T>> task)
    {
        return (await task.ConfigureAwait(false)).ValueOrThrow();
    }
}
