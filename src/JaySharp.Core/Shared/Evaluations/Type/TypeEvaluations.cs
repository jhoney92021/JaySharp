using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Type;

public class TypeEvaluation<T>
{
    public T ToEvaluate { get; set; }
    public bool ThrowException { get; set; }

    public TypeEvaluation(T toEvaluate, bool throwException)
    {
        ToEvaluate = toEvaluate;
        ThrowException = throwException;
    }
}

public static class TypeEvaluations
{
    /// <summary>
    /// Asserts that the evaluated object matches the target value or type.
    /// </summary>
    public static TypeEvaluation<T> Be<T>(this TypeEvaluation<T> toEvaluate, object toCompare)
    {
        bool isMatched = toEvaluate.ToEvaluate != null && toCompare != null &&
            (Equals(toEvaluate.ToEvaluate, toCompare) || toEvaluate.ToEvaluate.GetType() == toCompare.GetType());

        if (isMatched)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new TypeEvaluationException($"Must have been {toEvaluate.ToEvaluate} but was {toCompare}");
        }
        else
        {
            TestLogger.Warning($"Oughta been {toEvaluate.ToEvaluate} but was {toCompare}");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated object is of the same type as the target comparison object.
    /// </summary>
    public static TypeEvaluation<T> BeSameTypeAs<T>(this TypeEvaluation<T> toEvaluate, T toCompare)
    {
        bool isMatched = toEvaluate.ToEvaluate != null && toCompare != null &&
            (Equals(toEvaluate.ToEvaluate, toCompare) || toEvaluate.ToEvaluate.GetType() == toCompare.GetType());

        if (isMatched)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new TypeEvaluationException($"Must have been {toEvaluate.ToEvaluate} but was {toCompare}");
        }
        else
        {
            TestLogger.Warning($"Oughta been {toEvaluate.ToEvaluate} but was {toCompare}");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated object is not null.
    /// </summary>
    public static TypeEvaluation<T> NotBeNull<T>(this TypeEvaluation<T> toEvaluate)
    {
        if (toEvaluate.ToEvaluate != null)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new TypeEvaluationException("Must not be null, but was null.");
        }
        else
        {
            TestLogger.Warning("Oughta not be null, but was null.");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated object is null.
    /// </summary>
    public static TypeEvaluation<T> BeNull<T>(this TypeEvaluation<T> toEvaluate)
    {
        if (toEvaluate.ToEvaluate == null)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new TypeEvaluationException($"Must be null, but was {toEvaluate.ToEvaluate}.");
        }
        else
        {
            TestLogger.Warning($"Oughta be null, but was {toEvaluate.ToEvaluate}.");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated value is not null.
    /// </summary>
    public static (T Value, bool ThrowException) NotBeNull<T>(this (T Value, bool ThrowException) toEvaluate)
    {
        if (toEvaluate.Value != null)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new TypeEvaluationException("Must not be null, but was null.");
        }
        else
        {
            TestLogger.Warning("Oughta not be null, but was null.");
        }
        return toEvaluate;
    }

    /// <summary>
    /// Asserts that the evaluated value is null.
    /// </summary>
    public static (T Value, bool ThrowException) BeNull<T>(this (T Value, bool ThrowException) toEvaluate)
    {
        if (toEvaluate.Value == null)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new TypeEvaluationException($"Must be null, but was {toEvaluate.Value}.");
        }
        else
        {
            TestLogger.Warning($"Oughta be null, but was {toEvaluate.Value}.");
        }
        return toEvaluate;
    }
}