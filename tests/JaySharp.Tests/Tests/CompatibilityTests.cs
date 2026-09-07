using JaySharp.Compatibility;
using JaySharp.Compatibility.FluentAssertions;
using JaySharp.Compatibility.Shouldly;
using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

/// <summary>
/// Verifies xUnit, NUnit, and MSTest attribute alias compatibility.
/// </summary>
[TestFixture(On = Is.On)]
[TestClass(On = Is.On)]
public static class CompatibilityTests
{
    [Fact(On = Is.On)]
    public static void FactAttribute_ExecutesCleanly()
    {
        int value = 42;
        value.Must().Be(42);
    }

    [Test(On = Is.On)]
    public static void TestAttribute_ExecutesCleanly()
    {
        string name = "JaySharp";
        name.Must().Be("JaySharp");
    }

    [TestMethod(On = Is.On)]
    public static void TestMethodAttribute_ExecutesCleanly()
    {
        bool isAwesome = true;
        isAwesome.Must().Be(true);
    }

    [Fact(On = Is.On)]
    public static void FluentAssertions_Should_Be_ExecutesCleanly()
    {
        int value = 100;
        value.Should().Be(100);

        string title = "JaySharp";
        title.Should().Be("JaySharp");
    }

    [Fact(On = Is.On)]
    public static void Shouldly_ShouldBe_ExecutesCleanly()
    {
        int value = 100;
        value.ShouldBe(100);

        string title = "JaySharp";
        title.ShouldBe("JaySharp");

        bool isTrue = true;
        isTrue.ShouldBeTrue();
    }
}
