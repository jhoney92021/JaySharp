using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class IntegerTests
{
    [JayTest(Name = "CompareNumbers")]
    public static void CompareNumbers()
    {
        int underTest = 99;
        underTest.Oughta().Be(99);
    }
    [JayTest(Name = "CompareNumbers_Fail")]
    public static void CompareNumbers_Fail()
    {
        int underTest = 1;
        underTest.Oughta().Be(99);
    }
    [JayTest(Name = "CompareNumbers_Must_Be")]
    public static void CompareNumbers_Must_Be()
    {
        int underTest = 1;
        underTest.Must().Be(1);
    }
    [JayTest(Name = "CompareNumbers_Must_Be_Fail", On = Is.Off)]
    public static void CompareNumbers_Must_Be_Fail()
    {
        int underTest = 1;
        underTest.Must().Be(99);
    }
}