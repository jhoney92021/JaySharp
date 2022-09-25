namespace JaySharp.CommandLineArguments;

public class FirstArgument : Argument
{
    public FirstArgumentType ArgumentType {get;set;}
    public FirstArgument()
    {
        Value = string.Empty;
        ArgumentType = FirstArgumentType.None;
    }
}