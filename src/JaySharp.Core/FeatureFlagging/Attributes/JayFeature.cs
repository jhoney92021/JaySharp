using System.Diagnostics;
using JaySharp.TestSuite.TestAttributes;

namespace JaySharp.FeatureFlagging.Attributes;

/// <summary>
/// Tags a test suite or test method with a feature area identifier for selective test execution.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method, AllowMultiple = true)]
public class JayFeature : Attribute
{
    /// <summary>
    /// Gets or sets the feature tag name.
    /// </summary>
    public string Name { get; set; } = "unset";

    /// <summary>
    /// Gets or sets whether tests with this feature tag are enabled.
    /// </summary>
    public JaySharp.TestSuite.TestAttributes.Is On { get; set; } = JaySharp.TestSuite.TestAttributes.Is.On;

    /// <summary>
    /// Initializes a new instance of the <see cref="JayFeature"/> attribute.
    /// </summary>
    public JayFeature()
    {
        On = JaySharp.TestSuite.TestAttributes.Is.On;
        StackTrace stackTrace = new StackTrace();
        Name = stackTrace?.GetFrame(2)?.GetMethod()?.Name ?? "stack was null";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JayFeature"/> attribute with the specified feature name.
    /// </summary>
    /// <param name="name">The feature identifier.</param>
    public JayFeature(string name)
    {
        Name = name;
        On = JaySharp.TestSuite.TestAttributes.Is.On;
    }
}