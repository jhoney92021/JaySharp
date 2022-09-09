
using System.Diagnostics;
using JaySharp.Shared.Evaluations;

namespace JaySharp.TestSuite.TestAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)] 
public class JayTestSuite : Attribute
{
    public string Name {get;set;} = "unset";
    public Is On {get;set;} = Is.On;
    public JayTestSuite()
    {
        On = Is.On;
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }
}

public static class TurnTestSuiteOff
{
    public static void TurnOff(this JayTestSuite toTurnOff)
    {
        toTurnOff.On = Is.Off;
    }
}