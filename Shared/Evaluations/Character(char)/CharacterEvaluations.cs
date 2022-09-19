using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Characters;

public static class CharacterEvaluations
{
    public static void Be(this (char Value,bool ThrowException) toEvaluate, char toCompare)
    {
        if(toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new CharacterEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            TestLogger.Failed($"Oughta been {toEvaluate.Value} but was {toCompare}");
        }
    }
}