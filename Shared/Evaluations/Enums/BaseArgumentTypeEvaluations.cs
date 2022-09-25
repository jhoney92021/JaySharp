using JaySharp.CommandLineArguments;
using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Enum;

public static class BaseArgumentTypeEvaluations
{
    public static void Be(this (BaseArgumentType Value,bool ThrowException) toEvaluate, BaseArgumentType toCompare)
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