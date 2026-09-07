namespace JaySharp.TestSuite.TestRunner;
public static partial class TestRunner
{
    /// <summary>
    /// Discovers, loads configuration for, and runs all test suites in the target assembly.
    /// </summary>
    /// <returns>The total number of failed tests encountered during execution.</returns>
    public static int GetAndRunAllTestSuites()
    {
        TestsFailed = 0;
        JaySharp.Configuration.JaySharpConfigLoader.LoadConfig(TestSettings.ToTest);
        GetTestSuites();
        GetTests();
        RunTests();
        return TestsFailed;
    }
}