using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class OutcomeNilException : OutcomeFailureException
{
    internal OutcomeNilException(string message)
        : base(message)
    {
    }

    internal OutcomeNilException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}

public class Nil : Undefined
{
    internal Nil(string message) : base(new OutcomeNilException(message))
    {
    }

    internal Nil(string message, Exception innerEx) : base(new OutcomeNilException(message, innerEx))
    {
    }
}
