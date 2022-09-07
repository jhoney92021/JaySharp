using JaySharp.Evaluations.IEnumerable;

namespace JaySharp.Evaluations.Dictionaries;

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