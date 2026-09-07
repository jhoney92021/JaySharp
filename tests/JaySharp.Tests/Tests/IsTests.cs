using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Enum;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class IsTests
{
    [JayTest]
    public static void IsOn()
    {
        Is underTest = Is.On;
        underTest.Oughta().Be(Is.On);
    }
    [JayTest]
    public static void IsOn_Fail()
    {
        Is underTest = Is.On;
        underTest.Oughta().Be(Is.Off);
    }
    [JayTest]
    public static void IsOff()
    {
        Is underTest = Is.Off;
        underTest.Oughta().Be(Is.Off);
    }
    [JayTest]
    public static void IsOff_Fail()
    {
        Is underTest = Is.Off;
        underTest.Oughta().Be(Is.On);
    }
}