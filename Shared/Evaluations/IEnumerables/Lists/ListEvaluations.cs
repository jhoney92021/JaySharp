using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Lists;

public static partial class ListEvaluations
{
    public static (List<T>,bool) Oughta<T>(this List<T> toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (List<T>,bool) Must<T>(this List<T>  toEvaluate)
    {        
        return (toEvaluate, true);
    }
}