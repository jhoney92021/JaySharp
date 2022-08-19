using System.Reflection;
using JaySharp.ConsoleExtensions;

namespace JaySharp.TestRunner;

public static partial class TestRunner
{
    private static void GetTestSuites()
    {
        if(Assembly != null)
        {
            TestSuitesToRun = GetTypesWithAttribute(Assembly, TestSuiteType);
        }
    }

    private static void GetTests()
    {
        if(TestSuitesToRun != null)
        {
            TestsToRun = new List<MethodAndSuiteName>();
            foreach(var suite in TestSuitesToRun)
            {
                TestsToRun.AddRange(
                    GetMethodsWithAttribute(suite, TestType)
                );
            }
        }
    }

    private static void RunTests()
    {
        if(TestsToRun != null)
        {
            foreach(var methodAndSuiteName in TestsToRun)
            {
                var method = methodAndSuiteName.Method;
                var suitename = methodAndSuiteName.SuiteName;
                Logger.PrintWithColor($"Attempting to run {method.Name} from {suitename}", ConsoleColor.Yellow);
                try
                {
                    var parameters = method.GetParameters();
                    method.GetBaseDefinition().Invoke(null, parameters ?? null);
                }
                catch(Exception exception)
                {
                    TestLogger.Exception(exception?.InnerException?.ToString(), method.Name);
                    continue;
                }
            } 
        }
    }

    private static SuiteAndName[] GetTypesWithAttribute(Assembly assembly, Type attribute)
    {
        return assembly
                .GetTypes()
                .Where(type => type.GetCustomAttributes(attribute, true).Length > 0)
                .Select(type => new SuiteAndName{Type = type, Name = type.Name  })
                .ToArray();
    }

    private static MethodAndSuiteName[] GetMethodsWithAttribute(SuiteAndName suite, Type attribute)
    {
        return suite.Type
                .GetMethods()
                .Where(methodInfo=>methodInfo.GetCustomAttributes(attribute, true).Length > 0)
                .Select(methodInfo => new MethodAndSuiteName{Method = methodInfo, SuiteName = suite.Name })
                .ToArray();
    }
}