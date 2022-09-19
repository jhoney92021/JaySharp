namespace JaySharp.Shared.MethodExtensions;
public static partial class OughtaMethod
{ 
    public static (string,bool) Oughta(this string toEvaluate)
    {        
        return (toEvaluate, false);
    }
    public static (T[],bool) Oughta<T>(this T[] toEvaluate)
    {        
        return (toEvaluate, false);
    }
    public static (List<T>,bool) Oughta<T>(this List<T> toEvaluate)
    {        
        return (toEvaluate, false);
    }
    public static (Dictionary<TKey,TValue>,bool) Oughta<TKey,TValue>(this Dictionary<TKey,TValue> toEvaluate)
    {        
        return (toEvaluate, false);
    }
}