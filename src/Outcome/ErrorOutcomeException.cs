using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class ErrorOutcomeException : NilOutcomeException
{
    internal ErrorOutcomeException(string message)
        : base(message)
    {
    }

    internal ErrorOutcomeException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
