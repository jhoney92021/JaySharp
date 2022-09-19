using JaySharp.Shared.Evaluations.IEnumerable;
using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Dictionaries;

public static partial class DictionaryEvaluations
{
    public static void Be(this (Dictionary<int,int> Value,bool ThrowException) toEvaluate, Dictionary<int,int> toCompare)
    {
        var missing1 = toCompare.Except(toEvaluate.Value).ToDictionary(anon => anon.Key, anon => anon.Value);
        var missing2 = new Dictionary<int,int>();

        if(toEvaluate.Value.Count() != toCompare.Count())
        {
            missing2 = toEvaluate.Value.Except(toCompare).ToDictionary(anon => anon.Key, anon => anon.Value);
        } 
        
        if(missing1.Count() + missing2.Count() == 0)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new DictionaryEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            var toEvaluateMessage = BuildDictionaryMessage(toEvaluate.Value, IEnumerableComparisonMessageType.OughtaBeen);
            var evaluated = BuildDictionaryMessage(missing1.ToDictionary(anon => anon.Key, anon => anon.Value), IEnumerableComparisonMessageType.Evaluated);
            var compared = BuildDictionaryMessage(missing2.ToDictionary(anon => anon.Key, anon => anon.Value), IEnumerableComparisonMessageType.Compared);

            TestLogger.Failed(toEvaluateMessage + evaluated + compared);            
        }
    }
    private static string BuildDictionaryMessage(Dictionary<int,int> missing, IEnumerableComparisonMessageType messageType)
    {
        if(missing.Count() == 0){return string.Empty;}

        var message = DictionaryComparisonMessages.Messages[messageType];

        foreach(var number in missing)
        {
            message = message + $"{number} ";
        }

        message = message + "} \n";

        return message;
    }
}