using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Evaluations.Enum;
using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.Off)]
public static class BooleanTests
{
    [JayTest(On = Is.Off)]
    public static void IsTrue()
    {
        bool underTest = true;
        underTest.IsTrue();
    }
    [JayTest(On = Is.Off)]
    public static void IsTrue_Fail()
    {
        bool underTest = false;
        underTest.IsTrue();
    }
    [JayTest]
    public static void IsOn()
    {
        bool underTest = true;
        underTest.IsOn();
    }
    [JayTest]
    public static void IsOff()
    {
        bool underTest = false;
        underTest.IsOff();
    }
    [JayTest]
    public static void ConvertToInt()
    {
        bool underTest = false;
        int convertedValue = underTest.ConvertToInt();
        convertedValue.Oughta().Be(0);
    }
    [JayTest]
    public static void ConvertToIs()
    {
        bool underTest = false;
        Is convertedValue = underTest.ConvertToIs();
        convertedValue.Oughta().Be(Is.Off);
    }
}