namespace JaySharp.Evaluations.Lists;

public enum ArrayComparisonMessageType
{
    OughtaBeen,
    Evaluated,
    Compared
}

public static class ArrayComparisonMessages
{
    public static Dictionary<ArrayComparisonMessageType,string> Messages =
                new Dictionary<ArrayComparisonMessageType, string>
                {
                    {ArrayComparisonMessageType.OughtaBeen, "\nOughta been { "},
                    {ArrayComparisonMessageType.Evaluated ,  "evaluated list was missing { "},
                    {ArrayComparisonMessageType.Compared  ,  "compared list was missing { "}
                };
}