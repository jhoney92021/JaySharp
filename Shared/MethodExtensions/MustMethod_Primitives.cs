namespace JaySharp.Shared.MethodExtensions;
public static partial class MustMethod
{    
    public static (bool,bool) Must(this bool toEvaluate)
    {        
        return (toEvaluate, true);
    }    
    public static (int,bool) Must(this int toEvaluate)
    {        
        return (toEvaluate, true);
    }
    public static (char,bool) Must(this char toEvaluate)
    {        
        return (toEvaluate, true);
    }
}