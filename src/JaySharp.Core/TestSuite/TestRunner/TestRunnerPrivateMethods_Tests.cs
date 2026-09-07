using System.Reflection;
using JaySharp.FeatureFlagging.Attributes;
using JaySharp.Shared.Evaluations;
using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.IntermediateObjectDefinitions;

namespace JaySharp.TestSuite.TestRunner;

public static partial class TestRunner
{
    private static void GetTests()
    {
        if (TestSuitesToRun != null)
        {
            int index = 0;
            TestsToRun = new List<MethodAndSuiteName>();
            foreach (SuiteAndName suite in TestSuitesToRun)
            {
                if (ValidateSuiteIsOn(index)) TestsToRun.AddRange(GetMethodsWithAttribute(suite, TestType));
                TestsSuitesStarted++;
                index++;
            }
            JayLogger.PrintIfVerbose(Glyphes.Info($"Retrieved {TestsToRun.Count} Tests"), ConsoleColor.Yellow);
        }
    }

    private static void RunTests()
    {
        if (TestsToRun != null)
        {
            string currentSuiteName = string.Empty;
            int index = 0;
            foreach (MethodAndSuiteName testItem in TestsToRun)
            {
                if (currentSuiteName != testItem.SuiteName || string.IsNullOrEmpty(currentSuiteName))
                {
                    currentSuiteName = testItem.SuiteName;
                    JayLogger.PrintIfVerbose(Glyphes.Info($"Running {currentSuiteName}"), ConsoleColor.Blue);
                }

                try
                {
                    if (ValidateTestIsOn(index))
                    {
                        ParameterInfo[] parameters = testItem.Method.GetParameters();
                        try
                        {
                            testItem.Method.GetBaseDefinition().Invoke(null, parameters);
                        }
                        catch (Exception exception)
                        {
                            TestsCompleted--;
                            TestsFailed++;
                            if (exception.InnerException is EvaluationException)
                            {
                                TestLogger.Exception(exception.InnerException.ToString(), testItem.TestName ?? testItem.Method.Name);
                                continue;
                            }
                        }
                        finally
                        {
                            TestsCompleted++;
                        }
                    }
                    index++;
                    TestsStarted++;
                }
                catch (Exception exception)
                {
                    TestsStarted--;
                    TestsCompleted--;
                    TestsFailed++;
                    if (exception.InnerException is EvaluationException)
                    {
                        TestLogger.Exception(exception.InnerException.ToString(), testItem.TestName ?? testItem.Method.Name);
                        continue;
                    }
                }
            }
            JayLogger.PrintIfVerbose(Glyphes.Section($"{TestsSuitesStarted} Tests Suites Started"), ConsoleColor.Gray);
            JayLogger.PrintIfVerbose(Glyphes.Section($"{TestsStarted} Tests Started      "), ConsoleColor.Gray);
            JayLogger.PrintIfVerbose(Glyphes.Section($"{TestsCompleted} Tests Completed    "), ConsoleColor.Gray);
        }
    }

    private static MethodAndSuiteName[] GetMethodsWithAttribute(SuiteAndName suite, Type attribute)
    {
        return suite.Type
                .GetMethods()
                .Where(methodInfo => methodInfo.GetCustomAttributes(attribute, true).Length > 0)
                .Select(methodInfo => new MethodAndSuiteName
                {
                    Method = methodInfo,
                    TestName = methodInfo
                            .GetCustomAttributesData()
                            .SelectMany(attributeData => attributeData.NamedArguments.Where(namedArg => namedArg.MemberName == "Name"))
                            .FirstOrDefault().TypedValue.Value?.ToString() ?? "not found",
                    SuiteName = suite.Name
                })
                .ToArray();
    }

    private static bool ValidateTestIsOn(int idx)
    {
        if (TestsToRun == null) return false;
        if (TestsToRun.Count > idx)
        {
            MethodInfo methodInfo = TestsToRun[idx].Method;

            if (!string.IsNullOrEmpty(TestSettings.TargetFeature))
            {
                IEnumerable<JayFeature> methodFeatureAttributes = methodInfo.GetCustomAttributes(typeof(JayFeature), true)
                    .Cast<JayFeature>();
                if (methodFeatureAttributes.Any(feature => string.Equals(feature.Name, TestSettings.TargetFeature, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }

                IEnumerable<JayFeature>? declaringTypeFeatureAttributes = methodInfo.DeclaringType?.GetCustomAttributes(typeof(JayFeature), true)
                    .Cast<JayFeature>();
                return declaringTypeFeatureAttributes != null && declaringTypeFeatureAttributes.Any(feature => string.Equals(feature.Name, TestSettings.TargetFeature, StringComparison.OrdinalIgnoreCase));
            }

            if (TestSettings.RunAllTests) { return true; }

            IList<CustomAttributeData> customAttributes = methodInfo.GetCustomAttributesData();

            IEnumerable<CustomAttributeNamedArgument> namedArguments = customAttributes
                    .SelectMany(attributeData => attributeData.NamedArguments)
                    .Where(namedArg => namedArg.MemberName == "On");

            return !namedArguments.Any(namedArg => namedArg.TypedValue.Value?.ToString() == ((int)JaySharp.TestSuite.TestAttributes.Is.Off).ToString());
        }

        return false;
    }
}