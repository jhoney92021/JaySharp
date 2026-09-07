using System.Reflection;

namespace JaySharp.TestSuite.TestRunner;

public static partial class TestRunner
{
    public static Assembly? Assembly { get; set; } = Assembly.GetExecutingAssembly();
    public static Assembly? AssemblyToTest { get; set; } = Assembly.GetCallingAssembly();
    public static Assembly? AssemblyEntry { get; set; } = Assembly.GetEntryAssembly();

    /// <summary>
    /// Gets or sets the count of test methods that failed during execution.
    /// </summary>
    public static int TestsFailed { get; set; }
}