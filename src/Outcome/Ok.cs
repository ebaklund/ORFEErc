
namespace OutcomeCs;

public record Ok : Outcome;

public static class OkResultExtension
{
    public static Ok OkOrThrow(this Outcome outcome)
    {
        outcome.IncrementCheckCounter();

        return outcome switch
        {
            Ok ok => ok,
            _ => throw new System.InvalidCastException($"Input Result is not of type: Ok.")
        };
    }
}