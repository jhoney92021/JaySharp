using System.Runtime.Serialization;
using JaySharp.ConsoleExtensions;

namespace JaySharp.Evaluations.Integers;

public class IntegerEvaluationException : EvaluationException
{
    public IntegerEvaluationException()
    {
    }

    public IntegerEvaluationException(string? message) : base(message)
    {
        TestLogger.Failed(message);
    }

    public IntegerEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
        TestLogger.Failed(message);
    }

    protected IntegerEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}