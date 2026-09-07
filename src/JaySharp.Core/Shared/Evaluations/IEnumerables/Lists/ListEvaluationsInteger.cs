using JaySharp.Shared.Evaluations.IEnumerable;
using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Lists;

public static partial class ListEvaluations
{
    public static void Be(this (List<int> Value, bool ThrowException) toEvaluate, List<int> toCompare)
    {
        List<int> missing1 = toCompare.Except(toEvaluate.Value).ToList();
        List<int> missing2 = new List<int>();

        if (toEvaluate.Value.Count != toCompare.Count)
        {
            missing2 = toEvaluate.Value.Except(toCompare).ToList();
        }

        if (missing1.Count + missing2.Count == 0)
        {
            TestLogger.PassedInCyan();
        }
        else if (toEvaluate.ThrowException)
        {
            throw new ListEvaluationException($"Must have been {toEvaluate.Value} but was {toCompare}");
        }
        else
        {
            string toEvaluateMessage = BuildListMessage(toEvaluate.Value, IEnumerableComparisonMessageType.OughtaBeen);
            string evaluated = BuildListMessage(missing1, IEnumerableComparisonMessageType.Evaluated);
            string compared = BuildListMessage(missing2, IEnumerableComparisonMessageType.Compared);

            TestLogger.Failed(toEvaluateMessage + evaluated + compared);
        }
    }
    private static string BuildListMessage(List<int> missing, IEnumerableComparisonMessageType messageType)
    {
        if (missing.Count == 0) { return string.Empty; }

        string message = ListComparisonMessages.Messages[messageType];

        foreach (int number in missing)
        {
            message = message + $"{number} ";
        }

        message = message + "} \n";

        return message;
    }
}