namespace JaySharp.Configuration;

public class CodeStyleViolationException : Exception
{
    public CodeStyleViolationException()
    {
    }

    public CodeStyleViolationException(string? message) : base(message)
    {
    }

    public CodeStyleViolationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
