using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class UndefinedResultException : ResultException
{
    internal UndefinedResultException(string message)
        : base(message)
    {
    }

    internal UndefinedResultException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
