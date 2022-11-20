using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

public record Result
{
    internal Result()
    {
    }

    public static Result Ok()
    {
        return new Ok();
    }

    public static Result Error(string message)
    {
        return new Error(message);
    }

    public static Result Error(string message, Exception innerEx)
    {
        return new Error(message, innerEx);
    }
}
