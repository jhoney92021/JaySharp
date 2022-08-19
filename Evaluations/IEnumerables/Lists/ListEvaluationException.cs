using System.Runtime.Serialization;
using JaySharp.ConsoleExtensions;

namespace JaySharp.Evaluations.Lists;

public class ListEvaluationException : EvaluationException
{
    public ListEvaluationException()
    {
    }

    public ListEvaluationException(string? message) : base(message)
    {
        TestLogger.Failed(message);
    }

    public ListEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
        TestLogger.Failed(message);
    }

    protected ListEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}