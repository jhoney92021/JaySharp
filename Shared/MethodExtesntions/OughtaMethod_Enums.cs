using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Shared.MethodExtensions;

public static partial class OughtaMethod
{ 
    public static (Is,bool) Oughta(this Is toEvaluate)
    {        
        return (toEvaluate, false);
    }
}