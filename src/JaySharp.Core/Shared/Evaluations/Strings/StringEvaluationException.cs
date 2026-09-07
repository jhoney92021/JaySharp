using System.Runtime.Serialization;

namespace JaySharp.Shared.Evaluations.Strings;

public class StringEvaluationException : EvaluationException
{
    public StringEvaluationException()
    {
    }

    public StringEvaluationException(string? message) : base(message)
    {
    }

    public StringEvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}