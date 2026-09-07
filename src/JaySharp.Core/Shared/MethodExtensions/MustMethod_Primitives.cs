namespace JaySharp.Shared.MethodExtensions;

/// <summary>
/// Hard defensive guard extension methods (<c>.Must()</c>). Throws an exception if assertions fail.
/// </summary>
public static partial class MustMethod
{
    /// <summary>Initiates a hard defensive assertion for boolean values (<c>.Must()</c>).</summary>
    public static (bool Value, bool ThrowException) Must(this bool toEvaluate)
    {
        return (toEvaluate, true);
    }

    /// <summary>Initiates a hard defensive assertion for integer values (<c>.Must()</c>).</summary>
    public static (int Value, bool ThrowException) Must(this int toEvaluate)
    {
        return (toEvaluate, true);
    }

    /// <summary>Initiates a hard defensive assertion for character values (<c>.Must()</c>).</summary>
    public static (char Value, bool ThrowException) Must(this char toEvaluate)
    {
        return (toEvaluate, true);
    }
}