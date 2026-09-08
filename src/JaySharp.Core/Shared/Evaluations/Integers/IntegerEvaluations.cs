using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Integers;

public static class IntegerEvaluations
{
    /// <summary>
    /// Asserts that the evaluated integer matches the expected target value.
    /// </summary>
    public static (int Value, bool ThrowException) Be(this (int Value, bool ThrowException) toEvaluate, int toCompare)
    {
        if (toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new IntegerEvaluationException($"Must have been {toCompare} but was {toEvaluate.Value}");
        }
        else
        {
            TestLogger.Warning($"Oughta been {toCompare} but was {toEvaluate.Value}");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated integer is strictly greater than the minimum threshold.
    /// </summary>
    public static (int Value, bool ThrowException) BeGreaterThan(this (int Value, bool ThrowException) toEvaluate, int min)
    {
        if (toEvaluate.Value > min)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new IntegerEvaluationException($"Must be greater than {min} but was {toEvaluate.Value}");
        }
        else
        {
            TestLogger.Warning($"Oughta be greater than {min} but was {toEvaluate.Value}");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated integer is strictly less than the maximum threshold.
    /// </summary>
    public static (int Value, bool ThrowException) BeLessThan(this (int Value, bool ThrowException) toEvaluate, int max)
    {
        if (toEvaluate.Value < max)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new IntegerEvaluationException($"Must be less than {max} but was {toEvaluate.Value}");
        }
        else
        {
            TestLogger.Warning($"Oughta be less than {max} but was {toEvaluate.Value}");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated integer falls within the inclusive range [min, max].
    /// </summary>
    public static (int Value, bool ThrowException) BeBetween(this (int Value, bool ThrowException) toEvaluate, int min, int max)
    {
        if (toEvaluate.Value >= min && toEvaluate.Value <= max)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new IntegerEvaluationException($"Must be between {min} and {max} but was {toEvaluate.Value}");
        }
        else
        {
            TestLogger.Warning($"Oughta be between {min} and {max} but was {toEvaluate.Value}");
        }
        return toEvaluate;
    }
}