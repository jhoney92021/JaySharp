using System.Runtime.Serialization;

namespace JaySharp.Shared.Evaluations.Characters;

public class CharacterEvaluationException : EvaluationException
{
    public CharacterEvaluationException()
    {
    }

    public CharacterEvaluationException(string? message) : base(message)
    {
        
    }

    public CharacterEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {

    }

    protected CharacterEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}