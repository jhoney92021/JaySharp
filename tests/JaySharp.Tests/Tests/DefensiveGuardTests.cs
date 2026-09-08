using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.Evaluations.Type;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;
using Is = JaySharp.TestSuite.TestAttributes.Is;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.On, Description = "Validates defensive guard extension methods and fluent chaining.")]
public static class DefensiveGuardTests
{
    [JayTest(On = Is.On, Description = "NotBeNull asserts non-null object state.")]
    public static void NotBeNull_ExecutesCleanly()
    {
        string name = "Tama";
        name.Must().NotBeNull();
        name.Oughta().NotBeNull();
    }

    [JayTest(On = Is.On, Description = "BeNull asserts null object state.")]
    public static void BeNull_ExecutesCleanly()
    {
        string? name = null;
        name.Must().BeNull();
        name.Oughta().BeNull();
    }

    [JayTest(On = Is.On, Description = "Numeric range guards validate bounds.")]
    public static void NumericGuards_ExecuteCleanly()
    {
        int score = 42;
        score.Must().BeGreaterThan(0);
        score.Must().BeLessThan(100);
        score.Must().BeBetween(10, 50);

        score.Oughta().BeGreaterThan(10);
        score.Oughta().BeBetween(40, 50);
    }

    [JayTest(On = Is.On, Description = "String inspection guards validate prefixes, suffixes, and substrings.")]
    public static void StringGuards_ExecuteCleanly()
    {
        string url = "https://jaysharp.org/docs";
        url.Must().StartWith("https://");
        url.Must().EndWith("/docs");
        url.Must().Contain("jaysharp");

        url.Oughta().StartWith("https");
        url.Oughta().Contain("docs");
    }

    [JayTest(On = Is.On, Description = "Fluent chaining with .And() evaluates multiple assertions.")]
    public static void FluentChaining_ExecutesCleanly()
    {
        string petName = "Tama";
        petName.Must().NotBeNull().And().StartWith("Ta").And().EndWith("ma").And().Contain("am");

        int happiness = 85;
        happiness.Must().BeGreaterThan(50).And().BeLessThan(100).And().BeBetween(80, 90);
    }
}
