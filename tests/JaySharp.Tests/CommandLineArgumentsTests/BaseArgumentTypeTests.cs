using JaySharp.Shared.Evaluations.Enum;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.CommandLineArguments.Tests;

[JayTestSuite(On = Is.Off)]
public static class BaseArgumentTypeExtensionsTests
{
    [JayTest(On = Is.On)]
    public static void ToBaseArgumentType()
    {
        var underTest = "JaySharp";
        var result = underTest.ToBaseArgumentType();
        result.Oughta().Be(BaseArgumentType.JaySharp);
    }
}