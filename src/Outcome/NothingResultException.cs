using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class NothingResultException : UndefinedResultException
{
    internal NothingResultException(string message)
        : base(message)
    {
    }

    internal NothingResultException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
