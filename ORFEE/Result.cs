using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outcome;

class X<T> : X
{
}

class X
{
}

public class Result
{
    internal Result()
    {
    }

    public static Result Defined()
    {
        return new Defined();
    }

    public static Result Nil(string message)
    {
        return new Nil(message);
    }

    public static Result Nil(string message, Exception innerEx)
    {
        return new Nil(message, innerEx);
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
