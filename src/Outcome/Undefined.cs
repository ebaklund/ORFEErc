using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class OutcomeFailureException : ApplicationException
{
    internal OutcomeFailureException(string message)
        : base(message)
    {
    }

    internal OutcomeFailureException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}

public class Undefined : Result
{
    private OutcomeFailureException _reason;

    internal Undefined(OutcomeFailureException reason)
    {
        _reason = reason;
    }

    public OutcomeFailureException Reason 
    {
        get => _reason;
    }
}

