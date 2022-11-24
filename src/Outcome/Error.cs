
namespace OutcomeCs;

public record Error(ErrorOutcomeException Reason) : Outcome
{
    internal Error(string message) : this(new ErrorOutcomeException(message))
    {
    }

    internal Error(string message, Exception innerEx) : this(new ErrorOutcomeException(message, innerEx))
    {
    }
}

public static class ErrorOutcomeExtensions
{
    public static Error ErrorOrThrow(this Outcome outcome)
    {
        outcome.IncrementCheckCounter();

        return outcome switch
        {
            Error error => error,
            _ => throw new  System.InvalidCastException("Input Result is not of type: Error.")
        };
    }

    public static async Task<Error> ErrorOrThrowAsync(this Task<Outcome> task)
    {
        return (await task.ConfigureAwait(false)).ErrorOrThrow();
    }
}