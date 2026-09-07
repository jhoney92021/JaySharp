using JaySharp.Shared.Loggers;

namespace JaySharp.CommandLineArguments;

public enum BaseArgumentType
{
    None,
    JaySharp
}

public static class BaseArgumentTypeExtensions
{
    public static Dictionary<string, BaseArgumentType> AcceptedBaseArguments
    = new Dictionary<string, BaseArgumentType>(StringComparer.OrdinalIgnoreCase)
    {
        {"JaySharp", BaseArgumentType.JaySharp},
        {"Jay", BaseArgumentType.JaySharp},
        {"J", BaseArgumentType.JaySharp}
    };

    public static bool HasBaseArguement(this string[] toParse)
    {
        return toParse != null && toParse.Length > 2 && toParse[2] == "--";
    }

    public static BaseArgumentType ToBaseArgumentType(this string toParse)
    {
        if (string.IsNullOrEmpty(toParse)) return BaseArgumentType.None;
        return AcceptedBaseArguments.TryGetValue(toParse, out BaseArgumentType argumentType) ? argumentType : BaseArgumentType.None;
    }
}