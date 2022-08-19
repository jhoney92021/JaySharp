using JaySharp.Predicates.Integers;
using JaySharp.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite("IntegerEvaluationsTests")]
public static class IntegerEvaluationsTests
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
    [JayTest("CompareNumbers_Must_Be_Fail")]
    public static void CompareNumbers_Must_Be_Fail()
    {
        var underTest = 1;
        underTest.Must().Be(99);
    }
}