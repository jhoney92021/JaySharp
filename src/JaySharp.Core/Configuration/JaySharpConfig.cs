using JaySharp.Shared.Loggers;

namespace JaySharp.Configuration;

/// <summary>
/// Configuration settings for JaySharp code validation policies, log levels, and test execution behavior.
/// </summary>
public class JaySharpConfig
{
    private static JaySharpConfig _current = new();

    /// <summary>
    /// Gets or sets the global active JaySharp configuration instance.
    /// </summary>
    public static JaySharpConfig Current
    {
        get => _current;
        set => _current = value ?? new JaySharpConfig();
    }

    /// <summary>
    /// Gets or sets whether file-scoped namespaces are enforced. Default is true.
    /// </summary>
    public bool EnforceFileScopedNamespaces { get; set; } = true;

    /// <summary>
    /// Gets or sets whether explicit type declarations are required instead of implicit 'var'. Default is false.
    /// </summary>
    public bool EnforceExplicitVar { get; set; } = false;

    /// <summary>
    /// Gets or sets whether explicit visibility modifiers (public/private/protected) are required. Default is true.
    /// </summary>
    public bool EnforceExplicitVisibility { get; set; } = true;

    /// <summary>
    /// Gets or sets the default logging level during test execution.
    /// </summary>
    public LogLevel LogLevel { get; set; } = LogLevel.Standard;

    /// <summary>
    /// Gets or sets whether all discovered test suites run by default.
    /// </summary>
    public bool RunAllSuitesByDefault { get; set; } = true;

    /// <summary>
    /// Gets or sets whether all discovered test methods run by default.
    /// </summary>
    public bool RunAllTestsByDefault { get; set; } = true;
}
