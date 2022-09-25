using JaySharp.Shared.Loggers;

namespace JaySharp.CommandLineArguments;

public enum BaseArgumentType
{
    None,
    JaySharp
}

public static class BaseArgumentTypeExtensions
{
    public static Dictionary<BaseArgumentType, string> AcceptedBaseArguments
    = new Dictionary<BaseArgumentType, string>
    {
        {BaseArgumentType.JaySharp, "JaySharp"}
        ,{BaseArgumentType.JaySharp, "Jay"}
        ,{BaseArgumentType.JaySharp, "J"}
    };

    public static BaseArgumentType ToBaseArgumentType(this string toParse)
    {
        JayLogger.PrintInBlue(toParse);
        return BaseArgumentType.None;
    }
}