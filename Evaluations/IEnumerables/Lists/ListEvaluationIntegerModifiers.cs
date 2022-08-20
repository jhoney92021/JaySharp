using JaySharp.Loggers;

namespace JaySharp.Evaluations.Lists;

public static partial class ListEvaluations
{
    public static void Be(this (List<int> Value,bool ThrowException) toEvaluate, List<int> toCompare)
    {
        var missing1 = toCompare.Except(toEvaluate.Value).ToList();
        var missing2 = new List<int>();

        if(toEvaluate.Value.Count() != toCompare.Count())
        {
            missing2 = toEvaluate.Value.Except(toCompare).ToList();
        } 
        
        if(missing1.Count() + missing2.Count() == 0)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new ListEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            var toEvaluateMessage = BuildListMessage(toEvaluate.Value, ListComparisonMessageType.OughtaBeen);
            var evaluated = BuildListMessage(missing1, ListComparisonMessageType.Evaluated);
            var compared = BuildListMessage(missing2, ListComparisonMessageType.Compared);

            TestLogger.Failed(toEvaluateMessage + evaluated + compared);            
        }
    }
    private static string BuildListMessage(List<int> missing, ListComparisonMessageType messageType)
    {
        if(missing.Count() == 0){return string.Empty;}

        var message = ListComparisonMessages.Messages[messageType];

        foreach(var number in missing)
        {
            message = message + $"{number} ";
        }

        message = message + "} \n";

        return message;
    }
}