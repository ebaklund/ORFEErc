
using FluentAssertions;
using OutcomeCs;

namespace Readme.Tests;

class ListO<T>
{
    private List<T> _source;

    public ListO()
    {
        _source = new List<T>();
    }

    // With the guarantee that the resource getter aways succeeds, there is no reason to use Outcome.
    public int Capacity
    {
        get => _source.Capacity;
    }

    // The setter may fail, so we hide the original setter and create a setter method that returns an Outcome.
    public Outcome SetCapacity(int capacity)
    {
        try
        {
            _source.Capacity = capacity;
            return Outcome.Ok();
        }
        catch (Exception innerEx)
        {
            // Yeah, we catch everything. That ensures consistent Exception handling.
            // If the Outcome.Error is not properly checked, it will throw an UncheckedOutcomeException at Dispose() time.
            return Outcome.Error("Failed to set List<{}> Capacity", innerEx);
        }
    }
}


public class ListOTests
{
}

