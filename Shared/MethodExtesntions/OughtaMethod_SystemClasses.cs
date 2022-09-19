namespace JaySharp.Shared.MethodExtensions;
public static partial class OughtaMethod
{ 
    public static (string,bool) Oughta(this string toEvaluate)
    {        
        return (toEvaluate, false);
    }
}