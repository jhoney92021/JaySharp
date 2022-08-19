using System.Reflection;
using JaySharp.ConsoleExtensions;
using JaySharp.Evaluations.Integers;
using JaySharp.IntermediateObjectDefinitions;

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
            string currentSuiteName = string.Empty;
            foreach(var methodAndSuiteName in TestsToRun)
            {
                if(currentSuiteName != methodAndSuiteName.SuiteName || string.IsNullOrEmpty(currentSuiteName))
                {
                    currentSuiteName= methodAndSuiteName.SuiteName;
                    Logger.PrintWithColor($"~~ Running {currentSuiteName} ~~", ConsoleColor.Cyan);
                }
                var method = methodAndSuiteName.Method;
                try
                {
                    var parameters = method.GetParameters();
                    method.GetBaseDefinition().Invoke(null, parameters ?? null);
                }
                catch(Exception exception)
                {
                    if(exception.InnerException is IntegerEvaluationException)
                    {
                        TestLogger.Exception(exception?.InnerException?.ToString(), method.Name);
                        continue;
                    }
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