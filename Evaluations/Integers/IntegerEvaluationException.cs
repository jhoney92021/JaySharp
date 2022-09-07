using System.Runtime.Serialization;

namespace JaySharp.Evaluations.Integers;

public class IntegerEvaluationException : EvaluationException
{
    public IntegerEvaluationException()
    {
    }

    public IntegerEvaluationException(string? message) : base(message)
    {
        
    }

    public IntegerEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {

    }

    protected IntegerEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}