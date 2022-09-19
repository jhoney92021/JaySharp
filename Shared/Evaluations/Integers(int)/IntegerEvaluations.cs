using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Integers;

public static class IntegerEvaluations
{
    public static void Be(this (int Value,bool ThrowException) toEvaluate, int toCompare)
    {
        if(toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new IntegerEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            TestLogger.Failed($"Oughta been {toEvaluate.Value} but was {toCompare}");
        }
    }
}