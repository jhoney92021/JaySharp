using System.Diagnostics;
using JaySharp.TestSuite.TestRunner;

namespace JaySharp.Shared.Loggers;

/// <summary>
/// Provides utility methods for colored console logging and verbosity control.
/// </summary>
public class JayLogger
{
    /// <summary>
    /// Prints text to the console in blue color, prefixed with the caller method name.
    /// </summary>
    /// <param name="toPrint">The message string to print.</param>
    public static void PrintInBlue(string toPrint)
    {
        string calledTestMethod = new StackTrace().GetCallerMethodName();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[{calledTestMethod}] {toPrint}");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Prints text to the console in red color, prefixed with the caller method name.
    /// </summary>
    /// <param name="toPrint">The message string to print.</param>
    public static void PrintInRed(string toPrint)
    {
        string calledTestMethod = new StackTrace().GetCallerMethodName();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{calledTestMethod}] {toPrint}");
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Prints text to the console with the specified foreground color.
    /// </summary>
    /// <param name="toPrint">The message string to print.</param>
    /// <param name="printColor">The foreground color to use.</param>
    public static void PrintWithColor(string toPrint, ConsoleColor printColor)
    {
        Console.ForegroundColor = printColor;
        Console.WriteLine(toPrint);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Prints text to the console only if the global log level is set to <see cref="LogLevel.Verbose"/>.
    /// </summary>
    /// <param name="toPrint">The message string to print.</param>
    /// <param name="colorToPrint">The foreground color to use.</param>
    public static void PrintIfVerbose(string toPrint, ConsoleColor colorToPrint)
    {
        if (TestSettings.LogLevel == Loggers.LogLevel.Verbose)
        {
            PrintWithColor(toPrint, colorToPrint);
        }
    }
}