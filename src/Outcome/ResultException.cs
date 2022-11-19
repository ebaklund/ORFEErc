using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public class ResultException : ApplicationException
{
    internal ResultException(string message)
        : base(message)
    {
    }

    internal ResultException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}