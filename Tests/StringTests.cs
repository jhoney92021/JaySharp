using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.On)]
public static class StringTests
{
    [JayTest(Name = "CompareStrings")]
    public static void CompareStrings()
    {
        var underTest = "99";
        underTest.Oughta().Be("99");
    }
    // [JayTest(Name = "CompareStrings_Fail")]
    [JayTest(Name = "fdaf")]
    // [JayTest]
    public static void CompareStrings_Fail()
    {
        var underTest = "1";
        underTest.Oughta().Be("99");
    }
    [JayTest(Name = "CompareStrings_Must_Be")]
    public static void CompareStrings_Must_Be()
    {
        var underTest = "1";
        underTest.Must().Be("1");
    }
    // [JayTest(Name = "CompareStrings_Must_Be_Fail", On = Is.Off)]
    [JayTest(Name = "hi mom")]
    public static void CompareStrings_Must_Be_Fail()
    {
        var underTest = "1";
        underTest.Must().Be("99");
    }
}