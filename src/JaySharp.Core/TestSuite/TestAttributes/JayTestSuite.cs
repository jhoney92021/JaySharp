using System.Diagnostics;

namespace JaySharp.TestSuite.TestAttributes;

/// <summary>
/// Identifies a test suite class containing test methods.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class JayTestSuite : Attribute
{
    /// <summary>
    /// Gets or sets the custom name of the test suite. Defaults to the class name.
    /// </summary>
    public string Name { get; set; } = "unset";

    /// <summary>
    /// Gets or sets an optional descriptive summary of the suite's testing goal.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether this test suite is active (<see cref="Is.On"/>) or disabled (<see cref="Is.Off"/>).
    /// </summary>
    public Is On { get; set; } = Is.On;

    /// <summary>
    /// Initializes a new instance of the <see cref="JayTestSuite"/> attribute.
    /// </summary>
    public JayTestSuite()
    {
        On = Is.On;
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }
}