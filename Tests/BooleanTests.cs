using JaySharp.Predicates.Boolean;
using JaySharp.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite("BooleanTests")]
public static class BooleanTests
{
    [JayTest]
    public static void IsTrue()
    {
        var underTest = true;
        underTest.IsTrue();  
    }
    [JayTest]
    public static void IsTrue_Fail()
    {
        var underTest = false;
        underTest.IsTrue();  
    }
}