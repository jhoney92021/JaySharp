using System.Runtime.Serialization;
using JaySharp.ConsoleExtensions;

namespace JaySharp.Evaluations.Boolean;

public class BooleanEvaluationException : Exception
{
    public BooleanEvaluationException()
    {
    }

    public BooleanEvaluationException(string? message) : base(message)
    {
        TestLogger.Failed(message);
    }

    public BooleanEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
        TestLogger.Failed(message);
    }

    protected BooleanEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}