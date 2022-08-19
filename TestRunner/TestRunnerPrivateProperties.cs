using System.Reflection;
using JaySharp.TestAttributes;

namespace JaySharp.TestRunner;

public class SuiteAndName
{
    public Type Type {get;set;}
    public string Name {get;set;}
}
public struct MethodAndSuiteName
{
    public MethodInfo Method;
    public string SuiteName;
}

public static partial class TestRunner
{
    private static Type TestType {get;} = typeof(JayTest);
    private static Type TestSuiteType {get;} = typeof(JayTestSuite);
    private static Assembly? Assembly {get;} = Assembly.GetAssembly(typeof(JayTestSuite));
    private static SuiteAndName[]? TestSuitesToRun {get;set;}
    private static List<MethodAndSuiteName>? TestsToRun {get;set;}
}