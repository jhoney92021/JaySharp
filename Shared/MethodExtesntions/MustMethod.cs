namespace JaySharp.Shared.MethodExtensions;
public static class MustMethod
{    
    public static (char,bool) Must(this char toEvaluate)
    {        
        return (toEvaluate, true);
    }
}