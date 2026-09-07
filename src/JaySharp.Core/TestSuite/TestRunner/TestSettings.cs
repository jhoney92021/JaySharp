using JaySharp.Shared.Loggers;
using System.Reflection;


namespace JaySharp.TestSuite.TestRunner;

public static class TestSettings
{
    public static LogLevel LogLevel = LogLevel.Standard;
    public static bool RunAllSuites = false;
    public static bool RunAllTests = false;
    public static Assembly? ToTest = null;
    public static string? TargetFeature = null;

    public static bool Halp = false;
}