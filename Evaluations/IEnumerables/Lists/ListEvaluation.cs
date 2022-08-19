using JaySharp.ConsoleExtensions;

namespace JaySharp.Evaluations.Lists;

public static class ListEvaluations
{
    public static (List<T>,bool) Oughta<T>(this List<T> toEvaluate)
    {        
        return (toEvaluate, false);
    }
    
    public static (List<T>,bool) Must<T>(this List<T>  toEvaluate)
    {        
        return (toEvaluate, true);
    }

    public static void Be(this (List<int> Value,bool ThrowException) toEvaluate, List<int> toCompare)
    {
        var missing1 = toCompare.Except(toEvaluate.Value).ToList();
        var missing2 = toEvaluate.Value.Except(toCompare).ToList();
        
        if(missing1.Count() + missing2.Count() == 0)
        {
            TestLogger.PassedInBlue();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new ListEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }        
        else
        {
            var toEvaluateMessage = BuildOughtaEvaluatedListMessage(toEvaluate.Value);
            var offBy1 = BuildToEvaluateMissingListMessage(missing1);
            var offBy2 = BuildToCompareMissingListMessage(missing2);

            TestLogger.Failed(toEvaluateMessage + offBy1 + offBy2);
            // TestLogger.Failed($"Oughta been {toEvaluateMessage} but was off by {offBy}");
        }
    }

    private static string BuildOughtaEvaluatedListMessage(List<int> toEvaluate)
    {
        var message = "Oughta been";

        foreach(var number in toEvaluate)
        {
            message = message + $"\n {number}";
        }
        
        message = message + "\n";

        return message;
    }
    private static string BuildToEvaluateMissingListMessage(List<int> missing)
    {
        if(missing.Count() == 0){return string.Empty;}
        var message = "evaluated list was missing";

        foreach(var number in missing)
        {
            message = message + $"\n {number}";
        }

        message = message + "\n";

        return message;
    }
    private static string BuildToCompareMissingListMessage(List<int> missing)
    {
        if(missing.Count() == 0){return string.Empty;}
        var message = " compared list was missing";

        foreach(var number in missing)
        {
            message = message + $"\n {number}";
        }

        message = message + "\n";

        return message;
    }
}