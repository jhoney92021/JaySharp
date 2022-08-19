using System;
using System.Reflection;
using  JaySharp.Tests;

namespace JaySharp.TestRunner;

public static class TestRunner
{
    public static List<JayTestSuite> TestSuitesToRun {get;set;} = new List<JayTestSuite>();

    public static void BuildTestSuites()
    {   
        var testSuiteType = typeof(JayTestSuite);
        Assembly assembly = Assembly.GetAssembly(testSuiteType);
        Console.WriteLine(assembly);
        
        var test = GetTypesWithAttribute(assembly, testSuiteType);
        Console.WriteLine(test.FirstOrDefault());
        Console.WriteLine("DLKFJASDKF");

        var testType = typeof(JayTest);
        // Assembly assembly1 = Assembly.GetAssembly(testType);
        Console.WriteLine(assembly);
        
        var test1 = GetMethodsWithAttribute(test.FirstOrDefault(), testType);
        Console.WriteLine(test1.FirstOrDefault().Name);
        Console.WriteLine("9999999999");

    }

    static Type[] GetTypesWithAttribute(Assembly assembly, Type attribute) {
        return assembly.GetTypes().Where(m=>m.GetCustomAttributes(attribute, true).Length > 0).ToArray();
    }
    static MethodInfo[] GetMethodsWithAttribute(Type classType, Type attribute) {                
        return classType.GetMethods().Where(m=>m.GetCustomAttributes(attribute, true).Length > 0).ToArray();
    }
}