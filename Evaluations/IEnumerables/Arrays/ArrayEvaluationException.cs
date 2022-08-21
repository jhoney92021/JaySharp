using System.Runtime.Serialization;
using JaySharp.Loggers;

namespace JaySharp.Evaluations.Lists;

public class ArrayEvaluationException : EvaluationException
{
    public ArrayEvaluationException()
    {
    }

    public ArrayEvaluationException(string? message) : base(message)
    {
        TestLogger.Failed(message);
    }

    public ArrayEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
        TestLogger.Failed(message);
    }

    protected ArrayEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}