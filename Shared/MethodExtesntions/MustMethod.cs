namespace JaySharp.Shared.MethodExtensions;
public static class MustMethod
{    
    public static (int,bool) Must(this int toEvaluate)
    {        
        return (toEvaluate, true);
    }
    public static (char,bool) Must(this char toEvaluate)
    {        
        return (toEvaluate, true);
    }
}