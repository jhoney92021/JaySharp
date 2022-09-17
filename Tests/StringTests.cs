using JaySharp.Shared.Evaluations.Strings;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.On)]
public static class StringTests
{
    [JayTest("CompareStrings")]
    public static void CompareStrings()
    {
        var underTest = "99";
        underTest.Oughta().Be("99");
    }
    [JayTest("CompareStrings_Fail")]
    public static void CompareStrings_Fail()
    {
        var underTest = "1";
        underTest.Oughta().Be("99");
    }
    [JayTest("CompareStrings_Must_Be")]
    public static void CompareStrings_Must_Be()
    {
        var underTest = "1";
        underTest.Must().Be("1");
    }
    [JayTest("CompareStrings_Must_Be_Fail", On = Is.Off)]
    public static void CompareStrings_Must_Be_Fail()
    {
        var underTest = "1";
        underTest.Must().Be("99");
    }
}