using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Enum;

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
}