using JaySharp.ConsoleExtensions;
namespace JaySharp.Evaluations.Boolean;

public static class BooleanEvaluations
{
    public static void IsTrue(this bool toEvaluate)
    {
        if(toEvaluate != true)
        {
            TestLogger.Failed("Expected true but was false");
        }
        else
        {
            TestLogger.PassedInBlue();
        }
    }
    public static void IsFalse(this bool toEvaluate)
    {
        if(toEvaluate != false)
        {
            TestLogger.Failed("Expected false but was true");
        }
        else
        {
            TestLogger.PassedInBlue();
        }
    }
    public static void IsOn(this bool toEvaluate)
    {
        if(toEvaluate != true)
        {
            TestLogger.Failed("Expected to be on but was off");
        }
        else
        {
            TestLogger.PassedInBlue();
        }
    }
    public static void IsOff(this bool toEvaluate)
    {
        if(toEvaluate != false)
        {
            TestLogger.Failed("Expected to be off but was on");
        }
        else
        {
            TestLogger.PassedInBlue();
        }
    }
}