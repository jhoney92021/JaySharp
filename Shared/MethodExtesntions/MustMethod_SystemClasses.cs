namespace JaySharp.Shared.MethodExtensions;
public static partial class MustMethod
{
    public static (string,bool) Must(this string toEvaluate)
    {        
        return (toEvaluate, true);
    }
    public static (List<T>,bool) Must<T>(this List<T>  toEvaluate)
    {        
        return (toEvaluate, true);
    }
}