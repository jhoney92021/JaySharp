using System.Runtime.Serialization;
using JaySharp.ConsoleExtensions;

namespace JaySharp.Evaluations;

public class EvaluationException : Exception
{
    public EvaluationException()
    {
    }

    public EvaluationException(string? message) : base(message)
    {
        TestLogger.Failed(message);
    }

    public EvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
        TestLogger.Failed(message);
    }

    protected EvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}