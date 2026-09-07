using System.Diagnostics;

namespace JaySharp.TestSuite.TestAttributes;

/// <summary>
/// Identifies a test method or property within a JaySharp test suite.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class JayTest : Attribute
{
    /// <summary>
    /// Gets or sets the custom name of the test. Defaults to the method name.
    /// </summary>
    public string Name { get; set; } = "unset";

    /// <summary>
    /// Gets or sets an optional descriptive explanation of what this test is verifying.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether this test is active (<see cref="Is.On"/>) or disabled (<see cref="Is.Off"/>).
    /// </summary>
    public Is On { get; set; } = Is.On;

    /// <summary>
    /// Initializes a new instance of the <see cref="JayTest"/> attribute.
    /// </summary>
    public JayTest()
    {
        On = Is.On;
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }
}