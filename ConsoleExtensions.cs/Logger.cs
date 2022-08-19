using System.Diagnostics;

namespace JaySharp.ConsoleExtensions;

public class Logger
{
    public static void PrintInBlue(string toPrint)
    {
        StackTrace stackTrace = new StackTrace();
        var calledTestMethod = stackTrace?.GetFrame(2)?.GetMethod()?.Name;
        Console.ForegroundColor = ConsoleColor.Blue;        
        Console.WriteLine(toPrint);
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
    public static void PrintInRed(string toPrint)
    {
        StackTrace stackTrace = new StackTrace();
        var calledTestMethod = stackTrace?.GetFrame(2)?.GetMethod()?.Name;
        Console.ForegroundColor = ConsoleColor.Blue;        
        Console.WriteLine(toPrint);
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
    public static void PrintWithColor(string toPrint, ConsoleColor printColor)
    {
        Console.ForegroundColor = printColor;        
        Console.WriteLine(toPrint);
        Console.ForegroundColor = ConsoleColor.Gray;        
    }
}