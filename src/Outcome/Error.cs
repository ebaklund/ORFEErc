
namespace Outcome;

public record Error(ErrorOutcomeException Reason) : Outcome
{
    internal Error(string message) : this(new ErrorOutcomeException(message))
    {
    }

    internal Error(string message, Exception innerEx) : this(new ErrorOutcomeException(message, innerEx))
    {
    }
}

public static class ErrorResultExtension
{
    public static Error ErrorOrThrow(this Outcome result)
    {
        return result switch
        {
            Error error => error,
            _ => throw new  System.InvalidCastException("Input Result is not of type: Error.")
        };
    }
}