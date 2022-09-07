using JaySharp.Evaluations.IEnumerable;

namespace JaySharp.Evaluations.Lists;

public static class ArrayComparisonMessages
{
    public static Dictionary<IEnumerableComparisonMessageType,string> Messages =
                new Dictionary<IEnumerableComparisonMessageType, string>
                {
                    {IEnumerableComparisonMessageType.OughtaBeen, "\nOughta been { "},
                    {IEnumerableComparisonMessageType.Evaluated ,  "evaluated list was missing { "},
                    {IEnumerableComparisonMessageType.Compared  ,  "compared list was missing { "}
                };
}