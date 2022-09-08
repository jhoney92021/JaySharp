using JaySharp.Evaluations.Boolean;
using JaySharp.Evaluations.Integers;
using JaySharp.Evaluations.Enum;
using JaySharp.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite]
public static class BooleanTests
{
    [JayTest(On = Is.Off)]
    public static void IsTrue()
    {
        var underTest = true;
        underTest.IsTrue();  
    }
    [JayTest(On = Is.Off)]
    public static void IsTrue_Fail()
    {
        var underTest = false;
        underTest.IsTrue();  
    }
    [JayTest]
    public static void IsOn()
    {
        var underTest = true;
        underTest.IsOn();  
    }
    [JayTest]
    public static void IsOff()
    {
        var underTest = false;
        underTest.IsOff();  
    }
    [JayTest]
    public static void ConvertToInt()
    {
        var underTest = false;
        var convertedValue = underTest.ConvertToInt();  
        convertedValue.Oughta().Be(0);
    }
    [JayTest]
    public static void ConvertToIs()
    {
        var underTest = false;
        var convertedValue = underTest.ConvertToIs(); 
        convertedValue.Oughta().Be(Is.Off); 
    }
}