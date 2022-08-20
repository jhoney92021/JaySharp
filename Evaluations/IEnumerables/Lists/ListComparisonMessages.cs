namespace JaySharp.Evaluations.Lists;

public enum ListComparisonMessageType
{
    OughtaBeen,
    Evaluated,
    Compared
}

public static class ListComparisonMessages
{
    public static Dictionary<ListComparisonMessageType,string> Messages =
                new Dictionary<ListComparisonMessageType, string>
                {
                    {ListComparisonMessageType.OughtaBeen, "\nOughta been { "},
                    {ListComparisonMessageType.Evaluated ,  "evaluated list was missing { "},
                    {ListComparisonMessageType.Compared  ,  "compared list was missing { "}
                };
}