
namespace OutcomeCs;

public class UncheckedOutcomeException : OutcomeException
{
    internal UncheckedOutcomeException(CheckCountedOutcome uncheckedOutcome)
        : base("Outcome has not been checked.")
    {
        UncheckedOutcome = uncheckedOutcome;
    }

    public CheckCountedOutcome UncheckedOutcome
    {
        get;
    }
}
