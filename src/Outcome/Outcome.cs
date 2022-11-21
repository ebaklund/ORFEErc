using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public record Outcome
{
    internal Outcome()
    {
    }

    public static Outcome Ok()
    {
        return new Ok();
    }

    public static Outcome Error(string message)
    {
        return new Error(message);
    }

    public static Outcome Error(string message, Exception innerEx)
    {
        return new Error(message, innerEx);
    }
}
