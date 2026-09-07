namespace JaySharp.Shared.Evaluations;

public class EvaluationException : Exception
{
    public EvaluationException()
    {
    }

    public EvaluationException(string? message) : base(message)
    {
    }

    public EvaluationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}