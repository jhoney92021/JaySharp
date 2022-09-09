
using System.Diagnostics;
using JaySharp.Shared.Evaluations;

namespace JaySharp.TestSuite.TestAttributes;

[AttributeUsage(AttributeTargets.Method)] 
public class JayTest : Attribute
{
    public string Name {get;set;} = "unset";
    public Is On {get;set;} = Is.On;
    public JayTest()
    {
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }

    public JayTest(string name)
    {
        Name = name;
    }
}