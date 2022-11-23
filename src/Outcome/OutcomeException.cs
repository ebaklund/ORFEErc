
namespace OutcomeCs;

public class OutcomeException : ApplicationException
{
    private const string OutcomeStackTraceKey = "OutcomeStackTrace";

    private static void AttachStackTrace(Exception exception)
    {
        string envStackTrace = Environment.StackTrace;
        var nl = Environment.NewLine;
        var offset1 = Math.Max(0, envStackTrace.LastIndexOf("at OutcomeCs.Outcome"));
        var offset2 = envStackTrace.IndexOf(nl, offset1) + nl.Length;
        var stackTrace = envStackTrace.Substring(offset2);

        exception.Data[OutcomeStackTraceKey] = stackTrace;
    }

    internal OutcomeException(string message)
        : base(message)
    {
        AttachStackTrace(this);
    }

    internal OutcomeException(string message, Exception innerEx)
        : base(message, innerEx)
    {
        if (innerEx.InnerException == null)
        {
            AttachStackTrace(innerEx);
        }
    }

    public string OutcomeStackTrace
    {
        get 
        {
            var baseEx = this.GetBaseException();

            return (baseEx.Data.Contains(OutcomeStackTraceKey) && baseEx.Data[OutcomeStackTraceKey] is string stackTrace)
                ? stackTrace
                : "<no stack trace found>";
        }
    }

    public string OutcomeMessageTrace
    {
        get
        {
            var messageTrace = this.Message;

            for (Exception ex = this; ex.InnerException is not null; ex = ex.InnerException)
            {
            messageTrace += $"{Environment.NewLine}{ex.InnerException.Message}";
            }

            return messageTrace;
        }
    }
}