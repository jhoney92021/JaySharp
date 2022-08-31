using JaySharp.Loggers;

namespace JaySharp.Evaluations.Lists;

public static partial class ArrayEvaluations
{
    public static (T[],bool) Oughta<T>(this T[] toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (T[],bool) Must<T>(this T[] toEvaluate)
    {        
        return (toEvaluate, true);
    }
}