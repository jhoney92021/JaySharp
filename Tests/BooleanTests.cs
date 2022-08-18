using JaySharp.Predicates.Boolean;
namespace JaySharp.Tests;

public static class BooleanTests
{
    public static void IsTrue()
    {
        var underTest = true;
        underTest.IsTrue();  
    }
    public static void IsTrue_Fail()
    {
        var underTest = false;
        underTest.IsTrue();  
    }
}