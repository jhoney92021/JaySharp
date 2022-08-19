using JaySharp.ConsoleExtensions;

namespace JaySharp.Evaluations.Integers;

public static class IntegerEvaluations
{
    public static void Be(this int toEvaluate, int toCompare)
    {
        if(toEvaluate == toCompare)
        {
            TestLogger.PassedInBlue();
        }
        else
        {
            TestLogger.Failed($"Oughta been {toEvaluate} but was {toCompare}");
        }
    }

    public static (int,bool) Oughta(this int toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (int,bool) Must(this int toEvaluate)
    {        
        return (toEvaluate, true);
    }

    public static void Be(this (int Value,bool ThrowException) toEvaluate, int toCompare)
    {
        if(toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInBlue();
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