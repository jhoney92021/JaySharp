using JaySharp.Shared.Evaluations.Strings;
using JaySharp.TestSuite.TestAttributes;
using JaySharp.Shared.MethodExtensions;
using JaySharp.Shared.Evaluations.Type;

namespace JaySharp.Tests;
public class ToTest
{
    public int prop1 {get;set;}
    public bool prop2 {get;set;}
    public ToTest(int _prop1, bool _prop2)
    {
        prop1 = _prop1;
        prop2 = _prop2;
    }
}

[JayTestSuite(On = Is.Off)]
public static class TypeTests
{
    [JayTest(On = Is.On)]
    public static void CompareTypes()
    {
        var underTest = new ToTest(1,false);
        var matchedType = new ToTest(1,false);
        underTest.Oughta().Be(matchedType);        
    }
    [JayTest(On = Is.On)]
    public static void CompareTypes_Fail()
    {
        var underTest = new ToTest(1,false);        
        underTest.Oughta().Be("pp");        
    }
    [JayTest(On = Is.On)]
    public static void CompareTypes_Must()
    {
        var underTest = new ToTest(1,false);
        var matchedType = new ToTest(1,false);
        underTest.Must().Be(matchedType);        
    }
}
