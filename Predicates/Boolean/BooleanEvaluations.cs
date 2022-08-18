using JaySharp.ConsoleExtensions;
namespace JaySharp.Predicates.Boolean;

public static class BooleanEvaluations
{
    public static void IsTrue(this bool toEvaluate)
    {
        if(toEvaluate != true)
        {
            TestLogger.Failed($"Expected True but was {toEvaluate}");            
        }
        else
        {
            TestLogger.PassedInBlue();
        }
    }
}