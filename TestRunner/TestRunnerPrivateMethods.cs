using System.Reflection;
using JaySharp.Loggers;
using JaySharp.IntermediateObjectDefinitions;
using JaySharp.Evaluations;
using JaySharp.TestAttributes;

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
            Type currentSuite;
            int idx = 0;
            TestsToRun = new List<MethodAndSuiteName>();
            foreach(var suite in TestSuitesToRun)
            {

                currentSuite = TestSuitesToRun[idx].Type;
JayLogger.PrintWithColor($"~~ base {TestSuitesToRun[idx].Type.Attributes} ~~", ConsoleColor.Blue);
JayLogger.PrintWithColor($"~~ declare {TestSuitesToRun[idx].Type.GetCustomAttributesData()[0]} ~~", ConsoleColor.Blue);
JayLogger.PrintWithColor($"~~ declare {TestSuitesToRun[idx].Type.GetCustomAttributesData()[0].AttributeType} ~~", ConsoleColor.Green);
var ff = TestSuitesToRun[idx].Type.GetCustomAttributesData();
JayLogger.PrintWithColor($"~~ ff {ff.FirstOrDefault()} ~~", ConsoleColor.Green);
var fsf = ff.SelectMany(anon => anon.NamedArguments).Where(anon => anon.MemberName == "Off");
JayLogger.PrintWithColor($"~~ fsf {fsf.Count()} ~~", ConsoleColor.Yellow);
JayLogger.PrintWithColor($"~~ fsf {fsf.FirstOrDefault()} ~~", ConsoleColor.Green);
var bb = fsf.Select(na => na.TypedValue.Value);
JayLogger.PrintWithColor($"~~ bb {bb.FirstOrDefault()} ~~", ConsoleColor.Green);
JayLogger.PrintWithColor($"~~ decasdasdaslare {TestSuitesToRun[idx].Type.GetCustomAttribute(typeof(JayTestSuite))} ~~", ConsoleColor.Blue);
JayLogger.PrintWithColor($"~~ decasdasdaslare {TestSuitesToRun[idx].Type.GetCustomAttributesData().Select(anon => anon.NamedArguments[0].TypedValue.Value)} ~~", ConsoleColor.Red);
JayLogger.PrintWithColor($"~~ decasdasdaslare {TestSuitesToRun[idx].Type.GetCustomAttributesData().Select(anon => anon.NamedArguments[0].MemberInfo.Name)} ~~", ConsoleColor.Red);
                idx++;
JayLogger.PrintWithColor($"~~ is type of {currentSuite} ~~", ConsoleColor.Blue);
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
                    currentSuiteName = methodAndSuiteName.SuiteName;
                    JayLogger.PrintWithColor($"~~ Running {currentSuiteName} ~~", ConsoleColor.Blue);
                }
                var method = methodAndSuiteName.Method;
                try
                {
                    var parameters = method.GetParameters();
                    method.GetBaseDefinition().Invoke(null, parameters ?? null);
                }
                catch(Exception exception)
                {
                    if(exception.InnerException is EvaluationException)
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
                .Select(type => new SuiteAndName{Type = type, Name = type.Name })
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