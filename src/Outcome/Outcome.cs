
namespace OutcomeCs;

public record Outcome : CheckCountedOutcome
{
    internal Outcome()
    {
    }

    public static Outcome Ok()
    {
        return new Ok();
    }

    public static Outcome Error(string message)
    {
        return new Error(message);
    }

    public static Outcome Error(string message, Exception innerEx)
    {
        return new Error(message, innerEx);
    }
}
