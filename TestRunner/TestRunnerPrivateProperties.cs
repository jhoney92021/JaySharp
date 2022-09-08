using System.Reflection;
using JaySharp.TestAttributes;
using JaySharp.IntermediateObjectDefinitions;

namespace JaySharp.TestRunner;

public static partial class TestRunner
{
    private static Type TestType {get;} = typeof(JayTest);
    private static Type TestSuiteType {get;} = typeof(JayTestSuite);    
    private static SuiteAndName[]? TestSuitesToRun {get;set;}
    private static List<MethodAndSuiteName>? TestsToRun {get;set;}
    private static int TestsSuitesStarted {get;set;}
    private static int TestsStarted {get;set;}
}