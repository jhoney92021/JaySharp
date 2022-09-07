namespace JaySharp.TestAttributes;

public static class BoolToInt
{
    public static int ConvertToInt(this bool toConvert)
    {
        return toConvert ? 1 : 0;
    }
}