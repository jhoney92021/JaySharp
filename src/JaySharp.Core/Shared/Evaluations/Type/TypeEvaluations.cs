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
    public static void Be<T>(this TypeEvaluation<T> toEvaluate, object toCompare)
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
    }

    public static void BeSameTypeAs<T>(this TypeEvaluation<T> toEvaluate, T toCompare)
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
    }
}