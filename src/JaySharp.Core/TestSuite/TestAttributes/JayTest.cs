
using System.Diagnostics;

namespace JaySharp.TestSuite.TestAttributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class JayTest : Attribute
{
    public string Name { get; set; } = "unset";
    public Is On { get; set; } = Is.On;
    public JayTest()
    {
        On = Is.On;
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }
}