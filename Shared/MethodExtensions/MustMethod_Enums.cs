using JaySharp.CommandLineArguments;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Shared.MethodExtensions;

public static partial class MustMethod
{
    public static (Is,bool) Must(this Is toEvaluate)
    {        
        return (toEvaluate, true);
    }
    public static (BaseArgumentType,bool) Oughta(this BaseArgumentType toEvaluate)
    {        
        return (toEvaluate, false);
    }
}