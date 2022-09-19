using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Strings;

public static class StringEvaluations
{
    public static void Be(this string toEvaluate, string toCompare)
    {
        if(toEvaluate == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else
        {
            TestLogger.Failed($"Oughta been {toEvaluate} but was {toCompare}");
        }
    }

    public static (string,bool) Oughta(this string toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (string,bool) Must(this string toEvaluate)
    {        
        return (toEvaluate, true);
    }

    public static void Be(this (string Value,bool ThrowException) toEvaluate, string toCompare)
    {
        if(toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new StringEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            TestLogger.Failed($"Oughta been {toEvaluate.Value} but was {toCompare}");
        }
    }
}