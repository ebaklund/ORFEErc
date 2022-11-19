using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class ErrorResultException : NothingResultException
{
    internal ErrorResultException(string message)
        : base(message)
    {
    }

    internal ErrorResultException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
