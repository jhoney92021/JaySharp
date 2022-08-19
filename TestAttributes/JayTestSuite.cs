
using System.Diagnostics;
namespace JaySharp.TestAttributes;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)] 
public class JayTestSuite : Attribute
{
    public string Name {get;set;} = "unset";
    public JayTestSuite()
    {
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }
    public JayTestSuite(string name)
    {
        Name = name;
    }
}