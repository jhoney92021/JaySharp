using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Enum;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite]
public static class IsTests
{
    [JayTest]
    public static void IsOn()
    {
        var underTest = Is.On;
        underTest.Oughta().Be(Is.On);
    }
    [JayTest]
    public static void IsOn_Fail()
    {
        var underTest = Is.On;
        underTest.Oughta().Be(Is.Off);
    }
    [JayTest]
    public static void IsOff()
    {
        var underTest = Is.Off;
        underTest.Oughta().Be(Is.Off);
    }
    [JayTest]
    public static void IsOff_Fail()
    {
        var underTest = Is.Off;
        underTest.Oughta().Be(Is.On);
    }
}