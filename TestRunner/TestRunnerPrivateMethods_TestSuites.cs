using System.Reflection;
using JaySharp.IntermediateObjectDefinitions;
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

    private static SuiteAndName[] GetTypesWithAttribute(Assembly assembly, Type attribute)
    {
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
            var attributeData = TestSuitesToRun[idx].Type.GetCustomAttributesData();
            
            var namedArguments = attributeData
                    .SelectMany(anon => anon.NamedArguments)
                    .Where(anon => anon.MemberName == "On");         
            
            return !namedArguments.Any(na => na.TypedValue.Value?.ToString() == ((int)Is.Off).ToString());            
        }

        return false;
    }
}