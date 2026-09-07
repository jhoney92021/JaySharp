using JaySharp.Shared.Evaluations.IEnumerable;
using JaySharp.Shared.Loggers;

namespace JaySharp.Shared.Evaluations.Lists;

public static partial class ArrayEvaluations
{
    public static void Be(this (int[] Value, bool ThrowException) toEvaluate, int[] toCompare)
    {
        int[] missing1 = toCompare.Except(toEvaluate.Value).ToArray();
        int[] missing2 = Array.Empty<int>();

        if (toEvaluate.Value.Length != toCompare.Length)
        {
            missing2 = toEvaluate.Value.Except(toCompare).ToArray();
        }

        if (missing1.Length + missing2.Length == 0)
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

            TestLogger.Warning(toEvaluateMessage + evaluated + compared);
        }
    }
    private static string BuildListMessage(int[] missing, IEnumerableComparisonMessageType messageType)
    {
        if (missing.Length == 0) { return string.Empty; }

        string message = ArrayComparisonMessages.Messages[messageType];

        foreach (int number in missing)
        {
            message = message + $"{number} ";
        }

        message = message + "} \n";

        return message;
    }
}