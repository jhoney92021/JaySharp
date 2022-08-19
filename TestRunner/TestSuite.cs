using System;
namespace JaySharp.TestRunner;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)] 
public class JayTestSuite : Attribute
{
    public string Name {get;set;} = "unset";
    public static List<JayTest> TestsToRun {get;set;} = new List<JayTest>();
    public static List<TestResult> TestResults {get;set;} = new List<TestResult>();

    public JayTestSuite(string name)
    {
        Name = name;
        TestsToRun = new List<JayTest>();
        TestResults = new List<TestResult>();
    }
}