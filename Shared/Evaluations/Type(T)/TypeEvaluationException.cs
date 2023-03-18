using System.Runtime.Serialization;

namespace JaySharp.Shared.Evaluations.Type;

public class TypeEvaluationException : EvaluationException
{
    public TypeEvaluationException()
    {
    }

    public TypeEvaluationException(string? message) : base(message)
    {
        
    }

    public TypeEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {

    }

    protected TypeEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}