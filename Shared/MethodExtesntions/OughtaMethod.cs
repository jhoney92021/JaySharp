namespace JaySharp.Shared.MethodExtensions;
public static class OughtaMethod
{
    public static (int,bool) Oughta(this int toEvaluate)
    {        
        return (toEvaluate, false);
    }
    public static (char,bool) Oughta(this char toEvaluate)
    {        
        return (toEvaluate, false);
    }
}