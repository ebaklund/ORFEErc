using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class NilResultException : UndefinedResultException
{
    internal NilResultException(string message)
        : base(message)
    {
    }

    internal NilResultException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}
