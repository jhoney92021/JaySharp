using JaySharp.Shared.MethodExtensions;

namespace JaySharp.Compatibility.FluentAssertions;

/// <summary>
/// Opt-in fluent assertion extensions matching FluentAssertions syntax (.Should().Be()).
/// Scoped to JaySharp.Compatibility.FluentAssertions namespace to prevent conflicts if third-party FluentAssertions package is installed.
/// </summary>
public static class FluentAssertionsExtensions
{
    /// <summary>
    /// Wraps an integer in a JaySharp Must hard assertion chain.
    /// </summary>
    public static (int Value, bool ThrowException) Should(this int value)
    {
        return value.Must();
    }

    /// <summary>
    /// Wraps a boolean in a JaySharp Must hard assertion chain.
    /// </summary>
    public static (bool Value, bool ThrowException) Should(this bool value)
    {
        return value.Must();
    }

    /// <summary>
    /// Wraps a character in a JaySharp Must hard assertion chain.
    /// </summary>
    public static (char Value, bool ThrowException) Should(this char value)
    {
        return value.Must();
    }

    /// <summary>
    /// Wraps a string in a JaySharp Must hard assertion chain.
    /// </summary>
    public static (string Value, bool ThrowException) Should(this string value)
    {
        return (value, true);
    }
}
