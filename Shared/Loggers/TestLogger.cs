using System.Diagnostics;
using JaySharp.TestSuite.TestRunner;

namespace JaySharp.Shared.Loggers;

public static class TestLogger
{
    public static void PassedInCyan()
    {
        if(Settings.LogLevel != LogLevel.Succinct)
        {
            StackTrace stackTrace = new StackTrace();
            var calledTestMethod = stackTrace?.GetFrame(2)?.GetMethod()?.Name;
            Console.ForegroundColor = ConsoleColor.Cyan;        
            Console.WriteLine($"¡¡ {calledTestMethod} -- passed !!");
            Console.ForegroundColor = ConsoleColor.Gray;        
        }   
    }
    public static void Failed(string? failureReason)
    {
        StackTrace stackTrace = new StackTrace();
        var calledTestMethod = stackTrace?.GetFrame(2)?.GetMethod()?.Name;
        Console.ForegroundColor = ConsoleColor.Red;        
        Console.WriteLine($"¿¿ {calledTestMethod} -- failed -- {failureReason} ??");
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
    public static void Exception(string? failureReason, string method)
    {        
        Console.ForegroundColor = ConsoleColor.Red;        
        Console.WriteLine($"¿¿ {method} -- failed -- {failureReason} ??");
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
}