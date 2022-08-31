namespace JaySharp.Evaluations.Dictionaries;

public enum DictionaryComparisonMessageType
{
    OughtaBeen,
    Evaluated,
    Compared
}

public static class DictionaryComparisonMessages
{
    public static Dictionary<DictionaryComparisonMessageType,string> Messages =
                new Dictionary<DictionaryComparisonMessageType, string>
                {
                    {DictionaryComparisonMessageType.OughtaBeen, "\nOughta been { "},
                    {DictionaryComparisonMessageType.Evaluated ,  "evaluated Dictionary was missing { "},
                    {DictionaryComparisonMessageType.Compared  ,  "compared Dictionary was missing { "}
                };
}