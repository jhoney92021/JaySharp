namespace JaySharp.Shared.MethodExtensions;

/// <summary>
/// Soft defensive assertion extension methods (<c>.Oughta()</c>). Logs failure details without throwing exceptions.
/// </summary>
public static partial class OughtaMethod
{
    /// <summary>Initiates a soft defensive assertion for boolean values (<c>.Oughta()</c>).</summary>
    public static (bool Value, bool ThrowException) Oughta(this bool toEvaluate)
    {
        return (toEvaluate, false);
    }
    /// <summary>Initiates a soft defensive assertion for integer values (<c>.Oughta()</c>).</summary>
    public static (int Value, bool ThrowException) Oughta(this int toEvaluate)
    {
        return (toEvaluate, false);
    }
    /// <summary>Initiates a soft defensive assertion for character values (<c>.Oughta()</c>).</summary>
    public static (char Value, bool ThrowException) Oughta(this char toEvaluate)
    {
        return (toEvaluate, false);
    }
}