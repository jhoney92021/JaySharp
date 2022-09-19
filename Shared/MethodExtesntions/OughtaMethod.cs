namespace JaySharp.Shared.MethodExtensions;
public static class OughtaMethod
{
    public static (bool,bool) Oughta(this bool toEvaluate)
    {        
        return (toEvaluate, false);
    }
    public static (int,bool) Oughta(this int toEvaluate)
    {        
        return (toEvaluate, false);
    }
    public static (char,bool) Oughta(this char toEvaluate)
    {        
        return (toEvaluate, false);
    }    
    public static (string,bool) Oughta(this string toEvaluate)
    {        
        return (toEvaluate, false);
    }
}