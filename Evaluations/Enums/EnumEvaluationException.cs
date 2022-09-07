using System.Runtime.Serialization;
using JaySharp.Loggers;

namespace JaySharp.Evaluations.Enum;

public class EnumEvaluationException : Exception
{
    public EnumEvaluationException()
    {
    }

    public EnumEvaluationException(string? message) : base(message)
    {
        TestLogger.Failed(message);
    }

    public EnumEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
        TestLogger.Failed(message);
    }

    protected EnumEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}