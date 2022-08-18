using JaySharp.Predicates.Integers;
namespace JaySharp.Tests;

public static class IntegerEvaluationsTests
{
    public static void Is1()
    {
        var is1 = 1;

        is1.Is1();
    }
    public static void Is1_Fail()
    {
        var is1 = 0;

        is1.Is1();
    }

    public static void CompareNumbers()
    {
        var underTest = 99;
        underTest.Oughta().Be(99);
    }

    public static void CompareNumbers_Fail()
    {
        var underTest = 1;
        underTest.Oughta().Be(99);
    }
    public static void CompareNumbers_Must_Be()
    {
        var underTest = 1;
        underTest.Must().Be(1);
    }
    public static void CompareNumbers_Must_Be_Fail()
    {
        var underTest = 1;
        underTest.Must().Be(99);
    }
}