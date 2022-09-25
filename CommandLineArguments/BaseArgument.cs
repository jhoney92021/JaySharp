namespace JaySharp.CommandLineArguments;

public class BaseArgument : Argument
{
    public BaseArgumentType ArgumentType {get;set;}
    public BaseArgument()
    {
        Value = string.Empty;
        ArgumentType = BaseArgumentType.None;
    }
    // public BaseArgument(string argument, string argumentType)
    // {
    //     Value = argument;
    //     ArgumentType = Enum.TryParse<BaseArgumentType>(argumentType, true);
    // }
}