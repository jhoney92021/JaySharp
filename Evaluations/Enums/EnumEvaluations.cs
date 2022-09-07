using JaySharp.Loggers;
using JaySharp.TestAttributes;

namespace JaySharp.Evaluations.Enum;

public static class EnumEvaluations
{
    public static (Is,bool) Oughta(this Is toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (Is,bool) Must(this Is toEvaluate)
    {        
        return (toEvaluate, true);
    }

    public static void Be(this (Is Value,bool ThrowException) toEvaluate, Is toCompare)
    {
        if(toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new EnumEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            TestLogger.Failed($"Oughta been {toEvaluate.Value} but was {toCompare}");
        }
    }
}