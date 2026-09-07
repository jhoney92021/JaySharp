using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.Loggers;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;
using Is = JaySharp.TestSuite.TestAttributes.Is;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.On)]
public static class GlyphesTests
{
    [JayTest(On = Is.On)]
    public static void Glyphes_Pass_FormatsCorrectly()
    {
        string formatted = Glyphes.Pass("TestPassed");
        formatted.Oughta().Be("~~ TestPassed ~~");
    }

    [JayTest(On = Is.On)]
    public static void Glyphes_Fail_FormatsCorrectly()
    {
        string formatted = Glyphes.Fail("TestFailed");
        formatted.Oughta().Be("!! TestFailed !!");
    }

    [JayTest(On = Is.On)]
    public static void Glyphes_Info_FormatsCorrectly()
    {
        string formatted = Glyphes.Info("TestInfo");
        formatted.Oughta().Be("|| TestInfo ||");
    }

    [JayTest(On = Is.On)]
    public static void Glyphes_Section_FormatsCorrectly()
    {
        string formatted = Glyphes.Section("TestSection");
        formatted.Oughta().Be("|| TestSection ||");
    }
}
