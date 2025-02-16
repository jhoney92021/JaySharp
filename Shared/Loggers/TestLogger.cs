using System.Diagnostics;
using JaySharp.TestSuite.TestRunner;

namespace JaySharp.Shared.Loggers;

public static class TestLogger
{
    public static void PassedInCyan()
    {
        if(TestSettings.LogLevel != LogLevel.Succinct)
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
        var test = stackTrace?.GetFrame(2)?.GetMethod()?.GetCustomAttributesData()
                            .SelectMany(ad => ad.NamedArguments.Where(na => na.MemberName == "Name"))
                            .FirstOrDefault().TypedValue.Value?.ToString();
        Console.ForegroundColor = ConsoleColor.Red;        
        Console.WriteLine($"¿¿ {test ?? calledTestMethod} -- failed -- {failureReason} ??");
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
    public static void Exception(string? failureReason, string method)
    {        
        Console.ForegroundColor = ConsoleColor.DarkRed;        
        Console.WriteLine($"¿¿ {method} -- failed -- {failureReason} ??");
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
}