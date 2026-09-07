using System.Reflection;
using System.Text.Json;
using JaySharp.Shared.Loggers;

namespace JaySharp.Configuration;

/// <summary>
/// Loads JaySharp configuration settings from jaysharp.json or C# configuration conventions.
/// </summary>
public static class JaySharpConfigLoader
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Discovers and loads configuration settings from jaysharp.json and static setup methods.
    /// </summary>
    /// <param name="targetAssembly">Optional specific assembly to scan for setup methods.</param>
    /// <returns>The loaded <see cref="JaySharpConfig"/> instance.</returns>
    public static JaySharpConfig LoadConfig(Assembly? targetAssembly = null)
    {
        JaySharpConfig config = new();

        // 1. Check for jaysharp.json in current directory or assembly directory
        string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "jaysharp.json");
        if (File.Exists(jsonPath))
        {
            try
            {
                string jsonText = File.ReadAllText(jsonPath);
                JaySharpConfig? loadedConfig = JsonSerializer.Deserialize<JaySharpConfig>(jsonText, JsonOptions);
                if (loadedConfig != null)
                {
                    config = loadedConfig;
                }
            }
            catch (Exception exception)
            {
                JayLogger.PrintIfVerbose($"Warning: Could not parse jaysharp.json - {exception.Message}", ConsoleColor.Yellow);
            }
        }

        // 2. Discover C# setup method if defined in assembly
        Assembly[] assembliesToScan = targetAssembly != null
            ? new[] { targetAssembly }
            : AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !assembly.IsDynamic).ToArray();

        foreach (Assembly targetAsm in assembliesToScan)
        {
            try
            {
                MethodInfo? setupMethod = targetAsm.GetTypes()
                    .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                    .FirstOrDefault(method => method.Name == "ConfigureJaySharp" && method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(JaySharpConfig));

                if (setupMethod != null)
                {
                    setupMethod.Invoke(null, new object[] { config });
                    break;
                }
            }
            catch
            {
                // Ignore assemblies that cannot be scanned
            }
        }

        JaySharpConfig.Current = config;
        return config;
    }
}
