using JaySharp.Shared.Evaluations.IEnumerable;

namespace JaySharp.Shared.Evaluations.Dictionaries;

public static class DictionaryComparisonMessages
{
    public static Dictionary<IEnumerableComparisonMessageType,string> Messages =
                new Dictionary<IEnumerableComparisonMessageType, string>
                {
                    {IEnumerableComparisonMessageType.OughtaBeen, "\nOughta been { "},
                    {IEnumerableComparisonMessageType.Evaluated ,  "evaluated Dictionary was missing { "},
                    {IEnumerableComparisonMessageType.Compared  ,  "compared Dictionary was missing { "}
                };
}