using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.MethodExtensions;

namespace JaySharp.Compatibility.Shouldly;

/// <summary>
/// Opt-in fluent assertion extensions matching Shouldly syntax (.ShouldBe(expected)).
/// Scoped to JaySharp.Compatibility.Shouldly namespace to prevent conflicts if third-party Shouldly package is installed.
/// </summary>
public static class ShouldlyExtensions
{
    /// <summary>
    /// Evaluates whether the target integer equals the expected integer.
    /// </summary>
    public static void ShouldBe(this int value, int expected)
    {
        value.Must().Be(expected);
    }

    /// <summary>
    /// Evaluates whether the target string equals the expected string.
    /// </summary>
    public static void ShouldBe(this string value, string expected)
    {
        value.Must().Be(expected);
    }

    /// <summary>
    /// Evaluates whether the boolean value is true.
    /// </summary>
    public static void ShouldBeTrue(this bool condition)
    {
        condition.Must().Be(true);
    }

    /// <summary>
    /// Evaluates whether the boolean value is false.
    /// </summary>
    public static void ShouldBeFalse(this bool condition)
    {
        condition.Must().Be(false);
    }
}
