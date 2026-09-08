using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Strings;

public static class StringEvaluations
{
    /// <summary>
    /// Asserts that the evaluated string matches the target string.
    /// </summary>
    public static (string? Value, bool ThrowException) Be(this (string? Value, bool ThrowException) toEvaluate, string? toCompare)
    {
        if (toEvaluate.Value == toCompare)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new StringEvaluationException($"Must have been {toCompare} but was {toEvaluate.Value}");
        }
        else
        {
            TestLogger.Warning($"Oughta been {toCompare} but was {toEvaluate.Value}");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated string starts with the specified prefix.
    /// </summary>
    public static (string? Value, bool ThrowException) StartWith(this (string? Value, bool ThrowException) toEvaluate, string prefix)
    {
        if (toEvaluate.Value != null && toEvaluate.Value.StartsWith(prefix))
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new StringEvaluationException($"Must start with '{prefix}', but was '{toEvaluate.Value}'");
        }
        else
        {
            TestLogger.Warning($"Oughta start with '{prefix}', but was '{toEvaluate.Value}'");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated string ends with the specified suffix.
    /// </summary>
    public static (string? Value, bool ThrowException) EndWith(this (string? Value, bool ThrowException) toEvaluate, string suffix)
    {
        if (toEvaluate.Value != null && toEvaluate.Value.EndsWith(suffix))
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new StringEvaluationException($"Must end with '{suffix}', but was '{toEvaluate.Value}'");
        }
        else
        {
            TestLogger.Warning($"Oughta end with '{suffix}', but was '{toEvaluate.Value}'");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated string contains the specified substring.
    /// </summary>
    public static (string? Value, bool ThrowException) Contain(this (string? Value, bool ThrowException) toEvaluate, string substring)
    {
        if (toEvaluate.Value != null && toEvaluate.Value.Contains(substring))
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new StringEvaluationException($"Must contain '{substring}', but was '{toEvaluate.Value}'");
        }
        else
        {
            TestLogger.Warning($"Oughta contain '{substring}', but was '{toEvaluate.Value}'");
        }
        return toEvaluate;
    }
}