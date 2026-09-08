using System.Runtime.CompilerServices;
using JaySharp.Shared.Evaluations;
using JaySharp.Shared.Evaluations.Type;
using JaySharp.Shared.Loggers;

namespace JaySharp.Linq;

/// <summary>
/// Provides LINQ query syntax support (<c>from ... in ... where ... join ... on ... equals ... select</c>) for JaySharp test assertions.
/// </summary>
public static class JayLinqExtensions
{
    /// <summary>
    /// Evaluates a LINQ <c>where</c> clause predicate for object type evaluations.
    /// </summary>
    public static TypeEvaluation<T> Where<T>(
        this TypeEvaluation<T> source,
        Func<T, bool> predicate,
        [CallerArgumentExpression("predicate")] string? expression = null)
    {
        bool isSatisfied = source.ToEvaluate != null && predicate(source.ToEvaluate);

        if (isSatisfied)
        {
            TestLogger.PassedInCyan();
        }
        else if (source.ThrowException)
        {
            throw new TypeEvaluationException($"Must satisfy condition: {expression}");
        }
        else
        {
            TestLogger.Warning($"Oughta satisfy condition: {expression}");
        }
        return source;
    }

    /// <summary>
    /// Flushes a LINQ query evaluation and returns the evaluation container.
    /// </summary>
    public static TypeEvaluation<T> Select<T, TResult>(this TypeEvaluation<T> source, Func<T, TResult> selector)
    {
        return source;
    }

    /// <summary>
    /// Evaluates a LINQ <c>join ... on actual equals expected</c> equality clause for object type evaluations.
    /// </summary>
    public static TypeEvaluation<T> Join<T, TExpected, TKey>(
        this TypeEvaluation<T> source,
        TExpected expectedContainer,
        Func<T, TKey> outerKeySelector,
        Func<TExpected, TKey> innerKeySelector,
        Func<T, TExpected, T> resultSelector)
    {
        TKey actualKey = outerKeySelector(source.ToEvaluate);
        TKey expectedKey = innerKeySelector(expectedContainer);

        bool isMatched = Equals(actualKey, expectedKey);

        if (isMatched)
        {
            TestLogger.PassedInCyan();
        }
        else if (source.ThrowException)
        {
            throw new TypeEvaluationException($"Must have been equal to '{expectedKey}' but was '{actualKey}'");
        }
        else
        {
            TestLogger.Warning($"Oughta been equal to '{expectedKey}' but was '{actualKey}'");
        }
        return source;
    }

    /// <summary>
    /// Evaluates a LINQ <c>where</c> clause predicate for primitive value tuple evaluations.
    /// </summary>
    public static (T Value, bool ThrowException) Where<T>(
        this (T Value, bool ThrowException) source,
        Func<T, bool> predicate,
        [CallerArgumentExpression("predicate")] string? expression = null)
    {
        bool isSatisfied = predicate(source.Value);

        if (isSatisfied)
        {
            TestLogger.PassedInCyan();
        }
        else if (source.ThrowException)
        {
            throw new EvaluationException($"Must satisfy condition: {expression}");
        }
        else
        {
            TestLogger.Warning($"Oughta satisfy condition: {expression}");
        }
        return source;
    }

    /// <summary>
    /// Flushes a LINQ query evaluation and returns the primitive evaluation tuple container.
    /// </summary>
    public static (T Value, bool ThrowException) Select<T, TResult>(this (T Value, bool ThrowException) source, Func<T, TResult> selector)
    {
        return source;
    }

    /// <summary>
    /// Evaluates a LINQ <c>join ... on actual equals expected</c> equality clause for primitive value tuple evaluations.
    /// </summary>
    public static (T Value, bool ThrowException) Join<T, TExpected, TKey>(
        this (T Value, bool ThrowException) source,
        TExpected expectedContainer,
        Func<T, TKey> outerKeySelector,
        Func<TExpected, TKey> innerKeySelector,
        Func<T, TExpected, T> resultSelector)
    {
        TKey actualKey = outerKeySelector(source.Value);
        TKey expectedKey = innerKeySelector(expectedContainer);

        bool isMatched = Equals(actualKey, expectedKey);

        if (isMatched)
        {
            TestLogger.PassedInCyan();
        }
        else if (source.ThrowException)
        {
            throw new EvaluationException($"Must have been equal to '{expectedKey}' but was '{actualKey}'");
        }
        else
        {
            TestLogger.Warning($"Oughta been equal to '{expectedKey}' but was '{actualKey}'");
        }
        return source;
    }
}
