using JaySharp.Predicates.Boolean;
using JaySharp.TestRunner;

namespace JaySharp.Tests;

[JayTestSuite("BooleanTests")]
public static class BooleanTests
{
    [JayTest("IsTrue")]
    public static void IsTrue()
    {
        var underTest = true;
        underTest.IsTrue();  
    }
    [JayTest("IsTrue_Fail")]
    public static void IsTrue_Fail()
    {
        var underTest = false;
        underTest.IsTrue();  
    }
}