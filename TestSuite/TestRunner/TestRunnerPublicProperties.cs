using System.Reflection;
using JaySharp.TestSuite.TestAttributes;
using JaySharp.TestSuite.IntermediateObjectDefinitions;

namespace JaySharp.TestSuite.TestRunner;

public static partial class TestRunner
{
    public static Assembly? Assembly {get;set;} = Assembly.GetExecutingAssembly();
}