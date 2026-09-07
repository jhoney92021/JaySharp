using System.Diagnostics;
using System.Reflection;

namespace JaySharp.Shared.Loggers;

public static class StackTraceExtensions
{
    public static string GetCallerMethodName(this StackTrace stackTrace)
    {
        if (stackTrace == null) return "UnknownMethod";

        for (int i = 1; i < stackTrace.FrameCount; i++)
        {
            MethodBase? method = stackTrace.GetFrame(i)?.GetMethod();
            if (method == null) continue;

            Type? declaringType = method.DeclaringType;
            if (declaringType == null) continue;

            // Skip internal evaluation, logger, and extension frame methods
            if (declaringType.Namespace != null &&
                (declaringType.Namespace.StartsWith("JaySharp.Shared.Evaluations", StringComparison.Ordinal) ||
                 declaringType.Namespace.StartsWith("JaySharp.Shared.Loggers", StringComparison.Ordinal) ||
                 declaringType.Namespace.StartsWith("JaySharp.Shared.MethodExtensions", StringComparison.Ordinal)))
            {
                continue;
            }

            // Check if annotated with custom name in [JayTest(Name = "...")]
            string? customName = method.GetCustomAttributesData()
                .SelectMany(ad => ad.NamedArguments.Where(na => na.MemberName == "Name"))
                .FirstOrDefault().TypedValue.Value?.ToString();

            if (!string.IsNullOrEmpty(customName))
            {
                return customName!;
            }

            // Handle constructors (.ctor)
            if (method.IsConstructor || method.Name == ".ctor")
            {
                return $"{declaringType.Name} Constructor";
            }

            return method.Name;
        }

        return "UnknownMethod";
    }
}
