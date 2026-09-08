using JaySharp.Shared.Evaluations.Type;

namespace JaySharp.Shared.MethodExtensions;

/// <summary>
/// Provides fluent chaining extension methods (<c>.And()</c>) for combining multiple assertions in a single expression.
/// </summary>
public static class FluentChainingExtensions
{
    /// <summary>
    /// Flushes or continues assertion chaining on object type evaluations.
    /// </summary>
    public static TypeEvaluation<T> And<T>(this TypeEvaluation<T> evaluation)
    {
        return evaluation;
    }

    /// <summary>
    /// Flushes or continues assertion chaining on primitive evaluation tuples.
    /// </summary>
    public static (T Value, bool ThrowException) And<T>(this (T Value, bool ThrowException) evaluation)
    {
        return evaluation;
    }
}
