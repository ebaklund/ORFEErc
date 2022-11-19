
namespace Outcome;

public record Ok : Result;

public static class OkResultExtension
{
    public static Ok OkOrThrow(this Result result)
    {
        return result switch
        {
            Ok ok => ok,
            _ => throw new System.InvalidCastException($"Input Result is not of Ok type.")
        };
    }
}