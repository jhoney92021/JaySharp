using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.IntermediateObjectDefinitions;
using JaySharp.TestSuite.TestAttributes;
using System.Reflection;

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

        var entryAsm = Assembly.GetEntryAssembly();
        if (entryAsm != null)
        {
            foreach (var refAsmName in entryAsm.GetReferencedAssemblies())
            {
                try { Assembly.Load(refAsmName); } catch { }
            }
        }

        var assembliesToScan = TestSettings.ToTest != null
            ? new[] { TestSettings.ToTest }
            : AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .ToArray();

        var suites = new List<SuiteAndName>();
        foreach (var asm in assembliesToScan)
        {
            try
            {
                var found = GetTypesWithAttribute(asm, TestSuiteType);
                suites.AddRange(found);
            }
            catch
            {
                // Ignore assemblies that cannot be reflected over
            }
        }
        TestSuitesToRun = suites.ToArray();
        JayLogger.PrintIfVerbose($"~~ Retrieved {TestSuitesToRun.Length} Test Suites ~~", ConsoleColor.Yellow);
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
                            .SelectMany(ad => ad.NamedArguments.Where(na => na.MemberName == "Name"))
                            .FirstOrDefault().TypedValue.Value?.ToString()
                            ?? type.Name
                })
                .ToArray();
    }

    private static bool ValidateSuiteIsOn(int idx)
    {
        if (TestSuitesToRun == null) return false;
        if (TestSuitesToRun.Count() > idx)
        {
            if (TestSettings.RunAllSuites) { return true; }

            var attributeData = TestSuitesToRun[idx].Type.GetCustomAttributesData();

            var namedArguments = attributeData
                    .SelectMany(anon => anon.NamedArguments)
                    .Where(anon => anon.MemberName == "On");

            return !namedArguments.Any(na => na.TypedValue.Value?.ToString() == ((int)Is.Off).ToString());
        }

        return false;
    }
}