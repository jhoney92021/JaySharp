
using JaySharp.Loggers;

namespace JaySharp.Evaluations.Dictionaries;

public static partial class DictionaryEvaluations
{
    public static (Dictionary<T,T>,bool) Oughta<T>(this Dictionary<T,T> toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (Dictionary<T,T>,bool) Must<T>(this Dictionary<T,T>  toEvaluate)
    {        
        return (toEvaluate, true);
    }
}