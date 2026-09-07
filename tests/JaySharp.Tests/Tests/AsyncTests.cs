using JaySharp.Shared.Evaluations.Integers;
using JaySharp.Shared.Evaluations.Strings;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Tests;

/// <summary>
/// Verifies async Task test method execution and exception catching.
/// </summary>
[JayTestSuite(On = Is.On)]
public static class AsyncTests
{
    [JayTest(Description = "Async Task test method executes and awaits cleanly.", On = Is.On)]
    public static async Task AsyncTaskTest_ExecutesCleanly()
    {
        await Task.Delay(10);
        string result = "AsyncJaySharp";
        result.Must().Be("AsyncJaySharp");
    }

    [JayTest(Description = "Async Task test method evaluates integer assertions.", On = Is.On)]
    public static async Task AsyncTaskTest_EvaluatesIntegerAssertions()
    {
        await Task.Delay(5);
        int number = 100;
        number.Must().Be(100);
    }
}
