using System.Runtime.Serialization;
using JaySharp.Loggers;

namespace JaySharp.Evaluations.Dictionaries;

public class DictionaryEvaluationException : EvaluationException
{
    public DictionaryEvaluationException()
    {
    }

    public DictionaryEvaluationException(string? message) : base(message)
    {
        TestLogger.Failed(message);
    }

    public DictionaryEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
        TestLogger.Failed(message);
    }

    protected DictionaryEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}