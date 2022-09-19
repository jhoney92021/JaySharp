using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Strings;

public static class StringEvaluations
{
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