using JaySharp.Shared.Loggers;

namespace JaySharp.Configuration;

/// <summary>
/// Provides a seamless fluent configuration API and entry point for JaySharp test execution and policy configuration.
/// </summary>
public static class JaySharpRunner
{
    /// <summary>
    /// Configures JaySharp settings using an action delegate.
    /// </summary>
    /// <param name="configure">The configuration delegate.</param>
    /// <returns>The updated <see cref="JaySharpConfig"/> instance.</returns>
    public static JaySharpConfig Configure(Action<JaySharpConfig> configure)
    {
        JaySharpConfig config = JaySharpConfig.Current;
        configure?.Invoke(config);
        JaySharpConfig.Current = config;
        return config;
    }

    /// <summary>
    /// Configures JaySharp settings using a fluent builder chain.
    /// </summary>
    /// <param name="configure">The configuration action returning the modified config instance.</param>
    /// <returns>The updated <see cref="JaySharpConfig"/> instance.</returns>
    public static JaySharpConfig ConfigureFluent(Func<JaySharpConfig, JaySharpConfig> configure)
    {
        JaySharpConfig config = JaySharpConfig.Current;
        config = configure?.Invoke(config) ?? config;
        JaySharpConfig.Current = config;
        return config;
    }

    /// <summary>
    /// Enables or disables the explicit type declaration enforcement policy.
    /// </summary>
    /// <param name="config">The target configuration instance.</param>
    /// <param name="enable">Whether explicit type declarations are enforced. Defaults to true.</param>
    /// <returns>The modified <see cref="JaySharpConfig"/> instance for fluent chaining.</returns>
    public static JaySharpConfig WithExplicitVar(this JaySharpConfig config, bool enable = true)
    {
        config.EnforceExplicitVar = enable;
        return config;
    }

    /// <summary>
    /// Enables or disables file-scoped namespace enforcement.
    /// </summary>
    /// <param name="config">The target configuration instance.</param>
    /// <param name="enable">Whether file-scoped namespaces are enforced. Defaults to true.</param>
    /// <returns>The modified <see cref="JaySharpConfig"/> instance for fluent chaining.</returns>
    public static JaySharpConfig WithFileScopedNamespaces(this JaySharpConfig config, bool enable = true)
    {
        config.EnforceFileScopedNamespaces = enable;
        return config;
    }

    /// <summary>
    /// Sets the logging level for test execution output.
    /// </summary>
    /// <param name="config">The target configuration instance.</param>
    /// <param name="logLevel">The desired <see cref="LogLevel"/>.</param>
    /// <returns>The modified <see cref="JaySharpConfig"/> instance for fluent chaining.</returns>
    public static JaySharpConfig WithLogLevel(this JaySharpConfig config, LogLevel logLevel)
    {
        config.LogLevel = logLevel;
        return config;
    }

    /// <summary>
    /// Sets the log level to <see cref="LogLevel.Verbose"/>.
    /// </summary>
    /// <param name="config">The target configuration instance.</param>
    /// <returns>The modified <see cref="JaySharpConfig"/> instance for fluent chaining.</returns>
    public static JaySharpConfig WithVerboseLogging(this JaySharpConfig config)
    {
        config.LogLevel = LogLevel.Verbose;
        return config;
    }
}
