using JaySharp.Shared.Evaluations.Integers;
using JaySharp.TestSuite.TestAttributes;
using JaySharp.Shared.MethodExtensions;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class IntegerTests
{
    [JayTest("CompareNumbers")]
    public static void CompareNumbers()
    {
        var underTest = 99;
        underTest.Oughta().Be(99);
    }
    [JayTest("CompareNumbers_Fail")]
    public static void CompareNumbers_Fail()
    {
        var underTest = 1;
        underTest.Oughta().Be(99);
    }
    [JayTest("CompareNumbers_Must_Be")]
    public static void CompareNumbers_Must_Be()
    {
        var underTest = 1;
        underTest.Must().Be(1);
    }
    [JayTest("CompareNumbers_Must_Be_Fail", On = Is.Off)]
    public static void CompareNumbers_Must_Be_Fail()
    {
        var underTest = 1;
        underTest.Must().Be(99);
    }
}