using JaySharp.Shared.Evaluations.IEnumerable;

namespace JaySharp.Shared.Evaluations.Lists;

public static class ListComparisonMessages
{
    public static Dictionary<IEnumerableComparisonMessageType,string> Messages =
                new Dictionary<IEnumerableComparisonMessageType, string>
                {
                    {IEnumerableComparisonMessageType.OughtaBeen, "\nOughta been { "},
                    {IEnumerableComparisonMessageType.Evaluated ,  "evaluated list was missing { "},
                    {IEnumerableComparisonMessageType.Compared  ,  "compared list was missing { "}
                };
}