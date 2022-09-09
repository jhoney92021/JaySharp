using System.Diagnostics;

namespace JaySharp.FeatureFlagging.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)] 
public class JayFeature : Attribute
{
    public string Name {get;set;} = "unset";
    public Is On {get;set;} = Is.On;
    public JayFeature()
    {
        On = Is.On;
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }
}