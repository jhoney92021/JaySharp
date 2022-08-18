using System.Diagnostics;
namespace JaySharp.ConsoleExtensions;

public static class TestLogger
{
    public static void PassedInBlue()
    {
        StackTrace stackTrace = new StackTrace();
        var calledTestMethod = stackTrace?.GetFrame(2)?.GetMethod()?.Name;
        Console.ForegroundColor = ConsoleColor.Blue;        
        Console.WriteLine($"¡¡ {calledTestMethod} -- passed !!");
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
    public static void Failed(string? failureReason)
    {
        StackTrace stackTrace = new StackTrace();
        var calledTestMethod = stackTrace?.GetFrame(2)?.GetMethod()?.Name;
        Console.ForegroundColor = ConsoleColor.Red;        
        Console.WriteLine($"¿¿ {calledTestMethod} -- failed -- {failureReason} ??");
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
}