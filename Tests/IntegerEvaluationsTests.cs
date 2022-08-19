using JaySharp.TestRunner;
using JaySharp.Predicates.Integers;

namespace JaySharp.Tests;

[JayTestSuite("IntegerEvaluationsTests")]
public static class IntegerEvaluationsTests
{
    [JayTest("Is1")]
    public static void Is1()
    {
        var is1 = 1;

        is1.Is1();
    }
    [JayTest("Is1_Fail")]
    public static void Is1_Fail()
    {
        var is1 = 0;

        is1.Is1();
    }
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