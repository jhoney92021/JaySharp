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
        string underTest = "99";
        underTest.Oughta().Be("99");
    }
    // [JayTest(Name = "CompareStrings_Fail")]
    [JayTest(Name = "fdaf")]
    // [JayTest]
    public static void CompareStrings_Fail()
    {
        string underTest = "1";
        underTest.Oughta().Be("99");
    }
    [JayTest(Name = "CompareStrings_Must_Be")]
    public static void CompareStrings_Must_Be()
    {
        string underTest = "1";
        underTest.Must().Be("1");
    }
    [JayTest(Name = "hi mom", On = Is.Off)]
    public static void CompareStrings_Must_Be_Fail()
    {
        string underTest = "1";
        underTest.Must().Be("99");
    }
}