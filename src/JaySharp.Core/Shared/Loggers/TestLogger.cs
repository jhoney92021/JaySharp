using System.Diagnostics;
using JaySharp.TestSuite.TestRunner;

namespace JaySharp.Shared.Loggers;

/// <summary>
/// Provides logging helpers specifically formatted for test execution pass and fail output.
/// </summary>
public static class TestLogger
{
    /// <summary>
    /// Logs a test pass message in Cyan formatted with the pass glyph tag.
    /// </summary>
    public static void PassedInCyan()
    {
        if (TestSettings.LogLevel != LogLevel.Succinct)
        {
            string callerName = new StackTrace().GetCallerMethodName();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Glyphes.Pass($"{callerName} -- passed"));
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    /// <summary>
    /// Logs a soft warning message in Yellow formatted with the warning glyph tag.
    /// </summary>
    /// <param name="warningReason">Details of the soft warning.</param>
    public static void Warning(string? warningReason)
    {
        string callerName = new StackTrace().GetCallerMethodName();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Glyphes.Warning($"{callerName} -- warning -- {warningReason}"));
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Logs a test failure message in Red formatted with the fail glyph tag.
    /// </summary>
    /// <param name="failureReason">Details of the failure.</param>
    public static void Failed(string? failureReason)
    {
        string callerName = new StackTrace().GetCallerMethodName();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(Glyphes.Fail($"{callerName} -- failed -- {failureReason}"));
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Logs an unhandled exception failure message in DarkRed formatted with the fail glyph tag.
    /// </summary>
    /// <param name="failureReason">Exception message or details.</param>
    /// <param name="method">The method name where the exception occurred.</param>
    public static void Exception(string? failureReason, string method)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(Glyphes.Fail($"{method} -- failed -- {failureReason}"));
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}