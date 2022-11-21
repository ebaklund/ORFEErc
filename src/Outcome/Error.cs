
namespace Outcome;

public record Error(ErrorResultException Reason) : Outcome
{
    internal Error(string message) : this(new ErrorResultException(message))
    {
    }

    internal Error(string message, Exception innerEx) : this(new ErrorResultException(message, innerEx))
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