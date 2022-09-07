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
            int idx = 0;
            TestsToRun = new List<MethodAndSuiteName>();
            foreach(var suite in TestSuitesToRun)
            {                
                if(ValidateSuiteIsOn(idx)) TestsToRun.AddRange(GetMethodsWithAttribute(suite, TestType));  
                idx++;
            }
        }
    }

    private static void RunTests()
    {
        if(TestsToRun != null)
        {
            string currentSuiteName = string.Empty;
            int idx = 0;
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
                    if(ValidateTestIsOn(idx))
                    {
                        var parameters = method.GetParameters();
                        method.GetBaseDefinition().Invoke(null, parameters ?? null);
                    }
                    idx++;
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

    private static bool ValidateSuiteIsOn(int idx)
    {
        if(TestSuitesToRun == null) return false;
        if(TestSuitesToRun.Count() > idx)
        {
            var attributeData = TestSuitesToRun[idx].Type.GetCustomAttributesData();
            
            var namedArguments = attributeData
                    .SelectMany(anon => anon.NamedArguments)
                    .Where(anon => anon.MemberName == "On");         
            
            return !namedArguments.Any(na => na.TypedValue.Value?.ToString() == ((int)Is.Off).ToString());            
        }

        return false;
    }
    private static bool ValidateTestIsOn(int idx)
    {
        if(TestsToRun == null) return false;
        if(TestsToRun.Count() > idx)
        {
            var attributeData = TestsToRun[idx].Method.GetCustomAttributesData();
            
            var namedArguments = attributeData
                    .SelectMany(anon => anon.NamedArguments)
                    .Where(anon => anon.MemberName == "On");         
            
            return !namedArguments.Any(na => na.TypedValue.Value?.ToString() == ((int)Is.Off).ToString());            
        }

        return false;
    }
}