using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Dictionaries;

public static partial class DictionaryEvaluations
{
    public static (Dictionary<TKey,TValue>,bool) Oughta<TKey,TValue>(this Dictionary<TKey,TValue> toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (Dictionary<TKey,TValue>,bool) Must<TKey,TValue>(this Dictionary<TKey,TValue>  toEvaluate)
    {        
        return (toEvaluate, true);
    }
}