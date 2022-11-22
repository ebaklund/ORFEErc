using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class UndefinedOutcomeException : OutcomeException
{
    internal UndefinedOutcomeException(string message)
        : base(message)
    {
    }

    internal UndefinedOutcomeException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
