using System.Reflection;
using JaySharp.TestAttributes;
using JaySharp.IntermediateObjectDefinitions;

namespace JaySharp.TestRunner;

public static partial class TestRunner
{
    public static Assembly? Assembly {get;set;} = Assembly.GetExecutingAssembly();
}