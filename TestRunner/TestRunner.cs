using System;
using System.Reflection;
using  JaySharp.Tests;
using JaySharp.ConsoleExtensions;

namespace JaySharp.TestRunner;

public static class TestRunner
{
    private static Type TestType {get;} = typeof(JayTest);
    private static Type TestSuiteType {get;} = typeof(JayTestSuite);
    private static Assembly? Assembly {get;} = Assembly.GetAssembly(typeof(JayTestSuite));
    private static Type[]? TestSuitesToRun {get;set;}
    private static List<MethodInfo>? TestsToRun {get;set;}

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
            TestsToRun = new List<MethodInfo>();
            foreach(var suite in TestSuitesToRun)
            {
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
            foreach(var method in TestsToRun)
            {
                try
                {
                    var parameters = method.GetParameters();
                    method.GetBaseDefinition().Invoke(null, parameters ?? null);
                }
                catch(Exception exception)
                {
                    TestLogger.Failed(exception.InnerException.ToString());
                    continue;
                }
            } 
        }
    }

    public static void GetAndRunAllTestSuites()
    {
        GetTestSuites(); 
        GetTests(); 
        RunTests(); 
    }

    private static Type[] GetTypesWithAttribute(Assembly assembly, Type attribute)
    {
        return assembly.GetTypes().Where(m=>m.GetCustomAttributes(attribute, true).Length > 0).ToArray();
    }    
    private static MethodInfo[] GetMethodsWithAttribute(Type classType, Type attribute)
    {                
        return classType.GetMethods().Where(m=>m.GetCustomAttributes(attribute, true).Length > 0).ToArray();
    }
}