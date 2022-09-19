using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Shared.MethodExtensions;

public static partial class MustMethod
{
    public static (Is,bool) Must(this Is toEvaluate)
    {        
        return (toEvaluate, true);
    }
}