using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.Shared.Evaluations.Boolean;

public static class BooleanEvaluations
{
    public static void Be(this (bool Value,bool ThrowException) toEvaluate, bool toCompare)
    {
        if(toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new BooleanEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            TestLogger.Failed($"Oughta been {toEvaluate.Value} but was {toCompare}");
        }
    }

    public static void IsTrue(this bool toEvaluate)
    {
        if(toEvaluate != true)
        {
            TestLogger.Failed("Expected true but was false");
        }
        else
        {
            TestLogger.PassedInCyan();
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
            TestLogger.PassedInCyan();
        }
    }
    public static void IsOn(this bool toEvaluate)
    {
        if(toEvaluate.ConvertToIs() != Is.On)
        {
            TestLogger.Failed("Expected to be on but was off");
        }
        else
        {
            TestLogger.PassedInCyan();
        }
    }
    public static void IsOff(this bool toEvaluate)
    {
        if(toEvaluate.ConvertToIs() != Is.Off)
        {
            TestLogger.Failed("Expected to be off but was on");
        }
        else
        {
            TestLogger.PassedInCyan();
        }
    }

    public static int ConvertToInt(this bool toConvert)
    {
        return toConvert ? 1 : 0;
    }
    public static Is ConvertToIs(this bool toConvert)
    {
        return (Is)toConvert.ConvertToInt();
    }
}