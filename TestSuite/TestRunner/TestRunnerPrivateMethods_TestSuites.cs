using System.Reflection;
using JaySharp.TestSuite.IntermediateObjectDefinitions;
using JaySharp.TestSuite.TestAttributes;
using JaySharp.Shared.Loggers;

namespace JaySharp.TestSuite.TestRunner;

public static partial class TestRunner
{
    private static void GetTestSuites()
    {
        if(TestSettings.Halp)
        {
            JayLogger.PrintWithColor($"### ex {Assembly} ###", ConsoleColor.Yellow);
            JayLogger.PrintWithColor($"### call {AssemblyToTest} ###", ConsoleColor.Yellow);
            JayLogger.PrintWithColor($"### entry {AssemblyEntry} ###", ConsoleColor.Yellow);
        }
        if(AssemblyEntry != null)
        {
            TestSuitesToRun = GetTypesWithAttribute(AssemblyEntry, TestSuiteType);
            JayLogger.PrintIfVerbose($"~~ Retrieved {TestSuitesToRun.Count()} Test Suites ~~", ConsoleColor.Yellow);
        }
    }

    private static SuiteAndName[] GetTypesWithAttribute(Assembly assembly, Type attribute)
    {
        if(TestSettings.ToTest != null){assembly=TestSettings.ToTest;}
        return assembly
                .GetTypes()
                .Where(type => type.GetCustomAttributes(attribute, true).Length > 0)
                .Select(type => new SuiteAndName{Type = type, Name = type.Name })
                .ToArray();
    }

    private static bool ValidateSuiteIsOn(int idx)
    {
        if(TestSuitesToRun == null) return false;
        if(TestSuitesToRun.Count() > idx)
        {
            if(TestSettings.RunAllSuites){return true;}

            var attributeData = TestSuitesToRun[idx].Type.GetCustomAttributesData();
            
            var namedArguments = attributeData
                    .SelectMany(anon => anon.NamedArguments)
                    .Where(anon => anon.MemberName == "On");         
            
            return !namedArguments.Any(na => na.TypedValue.Value?.ToString() == ((int)Is.Off).ToString());            
        }

        return false;
    }
}