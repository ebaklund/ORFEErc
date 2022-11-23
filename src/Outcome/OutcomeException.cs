

using System.Text;

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
        var baseEx = this.GetBaseException();

        if (!baseEx.Data.Contains(OutcomeStackTraceKey))
        {
            AttachStackTrace(baseEx);
        }
    }

    public string? OutcomeStackTrace
    {
        get 
        {
            return this.GetBaseException().Data[OutcomeStackTraceKey] as string;
        }
    }

    public string OutcomeMessageTrace
    {
        get
        {
            var messageTrace = new StringBuilder(this.Message);

            for (Exception ex = this; ex.InnerException is not null; ex = ex.InnerException)
            {
                messageTrace.Append($"{Environment.NewLine}{ex.InnerException.Message}");
            }

            return messageTrace.ToString();
        }
    }
}