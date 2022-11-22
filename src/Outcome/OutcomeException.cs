using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutcomeCs;

public class OutcomeException : ApplicationException
{
    internal OutcomeException(string message)
        : base(message)
    {
    }

    internal OutcomeException(string message, Exception innerEx)
        : base(message, innerEx)
    {
    }
}