using System.Reflection;
using JaySharp.FeatureFlagging.Attributes;
using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.IntermediateObjectDefinitions;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.TestSuite.TestRunner;

public static partial class TestRunner
{
    private static void GetTestSuites()
    {
        if (TestSettings.Halp)
        {
            JayLogger.PrintWithColor($"### ex {Assembly} ###", ConsoleColor.Yellow);
            JayLogger.PrintWithColor($"### call {AssemblyToTest} ###", ConsoleColor.Yellow);
            JayLogger.PrintWithColor($"### entry {AssemblyEntry} ###", ConsoleColor.Yellow);
        }

        Assembly? entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            foreach (AssemblyName referencedAssemblyName in entryAssembly.GetReferencedAssemblies())
            {
                try { Assembly.Load(referencedAssemblyName); } catch { }
            }
        }

        Assembly[] assembliesToScan = TestSettings.ToTest != null
            ? new[] { TestSettings.ToTest }
            : AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly => !assembly.IsDynamic)
                .ToArray();

        List<SuiteAndName> suites = new();
        foreach (Assembly targetAsm in assembliesToScan)
        {
            try
            {
                SuiteAndName[] foundSuites = GetTypesWithAttribute(targetAsm, TestSuiteType);
                suites.AddRange(foundSuites);
            }
            catch
            {
                // Ignore assemblies that cannot be reflected over
            }
        }
        TestSuitesToRun = suites.ToArray();
        JayLogger.PrintIfVerbose(Glyphes.Info($"Retrieved {TestSuitesToRun.Length} Test Suites"), ConsoleColor.Yellow);
    }

    private static SuiteAndName[] GetTypesWithAttribute(Assembly assembly, Type attribute)
    {
        if (TestSettings.ToTest != null) { assembly = TestSettings.ToTest; }
        return assembly
                .GetTypes()
                .Where(type => type.GetCustomAttributes(attribute, true).Length > 0)
                .Select(type => new SuiteAndName
                {
                    Type = type,
                    Name = type
                            .GetCustomAttributesData()
                            .SelectMany(attributeData => attributeData.NamedArguments.Where(namedArg => namedArg.MemberName == "Name"))
                            .FirstOrDefault().TypedValue.Value?.ToString()
                            ?? type.Name
                })
                .ToArray();
    }

    private static bool ValidateSuiteIsOn(int idx)
    {
        if (TestSuitesToRun == null) return false;
        if (TestSuitesToRun.Length > idx)
        {
            Type suiteType = TestSuitesToRun[idx].Type;

            if (!string.IsNullOrEmpty(TestSettings.TargetFeature))
            {
                IEnumerable<JayFeature> featureAttributes = suiteType.GetCustomAttributes(typeof(JayFeature), true)
                    .Cast<JayFeature>();
                return featureAttributes.Any(feature => string.Equals(feature.Name, TestSettings.TargetFeature, StringComparison.OrdinalIgnoreCase));
            }

            if (TestSettings.RunAllSuites) { return true; }

            IList<CustomAttributeData> customAttributes = suiteType.GetCustomAttributesData();

            IEnumerable<CustomAttributeNamedArgument> namedArguments = customAttributes
                    .SelectMany(attributeData => attributeData.NamedArguments)
                    .Where(namedArg => namedArg.MemberName == "On");

            return !namedArguments.Any(namedArg => namedArg.TypedValue.Value?.ToString() == ((int)JaySharp.TestSuite.TestAttributes.Is.Off).ToString());
        }

        return false;
    }
}