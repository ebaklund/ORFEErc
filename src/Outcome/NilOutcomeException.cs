using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class NilOutcomeException : UndefinedOutcomeException
{
    internal NilOutcomeException(string message)
        : base(message)
    {
    }

    internal NilOutcomeException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
