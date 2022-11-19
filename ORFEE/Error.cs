using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class OutcomeErrorException : OutcomeFailureException
{
    internal OutcomeErrorException(string message)
        : base(message)
    {
    }

    internal OutcomeErrorException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}

public class Error : Undefined
{
    internal Error(string message) : base(new OutcomeErrorException(message))
    {    
    }

    internal Error(string message, Exception innerEx) : base(new OutcomeErrorException(message, innerEx))
    {
    }
}

public static class ResultExctension
{
    public static Error ErrorOrThrow(this Result result)
    {
        return result switch
        {
            Error error => error,
            _ => throw new  System.InvalidCastException("Input Result is not of Error type.")
        };
    }
}