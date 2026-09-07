using JaySharp.Configuration;
using JaySharp.FeatureFlagging.Attributes;
using JaySharp.Shared.Evaluations.Boolean;
using JaySharp.Shared.Loggers;
using JaySharp.Shared.MethodExtensions;
using JaySharp.TestSuite.TestAttributes;
using Is = JaySharp.TestSuite.TestAttributes.Is;

namespace JaySharp.Tests;

[JayTestSuite(On = Is.On)]
[JayFeature("ConfigFallback")]
public static class ConfigurationTests
{
    [JayTest(On = Is.On)]
    public static void DefaultConfig_ReturnsFallbackDefaults()
    {
        JaySharpConfig config = JaySharpConfigLoader.LoadConfig();

        config.EnforceFileScopedNamespaces.Oughta().Be(true);
        config.EnforceExplicitVar.Oughta().Be(false);
        config.EnforceExplicitVisibility.Oughta().Be(true);
        (config.LogLevel == LogLevel.Standard).Oughta().Be(true);
        config.RunAllSuitesByDefault.Oughta().Be(true);
        config.RunAllTestsByDefault.Oughta().Be(true);
    }

    [JayTest(On = Is.On)]
    public static void Config_GlobalCurrentInstanceSet()
    {
        JaySharpConfig loaded = JaySharpConfigLoader.LoadConfig();
        JaySharpConfig current = JaySharpConfig.Current;

        (current != null).Oughta().Be(true);
        current!.EnforceFileScopedNamespaces.Oughta().Be(loaded.EnforceFileScopedNamespaces);
    }

    [JayTest(On = Is.On)]
    public static void Config_PropertyOverrides()
    {
        JaySharpConfig config = new JaySharpConfig
        {
            EnforceExplicitVar = true,
            LogLevel = LogLevel.Verbose
        };

        JaySharpConfig.Current = config;

        JaySharpConfig.Current.EnforceExplicitVar.Oughta().Be(true);
        (JaySharpConfig.Current.LogLevel == LogLevel.Verbose).Oughta().Be(true);
    }

    [JayTest(On = Is.On)]
    public static void EnforceExplicitVar_True_RejectsVarDeclaration()
    {
        JaySharpConfig config = new JaySharpConfig { EnforceExplicitVar = true };
        string sampleCode = "public void Test() { var number = 42; }";

        bool isValid = JaySharpCodeValidator.ValidateExplicitVar(sampleCode, config);
        isValid.Oughta().Be(false);
    }

    [JayTest(On = Is.On)]
    public static void EnforceExplicitVar_False_AllowsVarDeclaration()
    {
        JaySharpConfig config = new JaySharpConfig { EnforceExplicitVar = false };
        string sampleCode = "public void Test() { var number = 42; }";

        bool isValid = JaySharpCodeValidator.ValidateExplicitVar(sampleCode, config);
        isValid.Oughta().Be(true);
    }

    [JayTest(On = Is.On)]
    public static void EnforceExplicitVar_True_AllowsExplicitTypes()
    {
        JaySharpConfig config = new JaySharpConfig { EnforceExplicitVar = true };
        string sampleCode = "public void Test() { int number = 42; }";

        bool isValid = JaySharpCodeValidator.ValidateExplicitVar(sampleCode, config);
        isValid.Oughta().Be(true);
    }

    [JayTest(On = Is.On)]
    public static void EnforceExplicitVar_True_ThrowsExceptionOnVarDeclaration()
    {
        JaySharpConfig config = new JaySharpConfig { EnforceExplicitVar = true };
        string sampleCode = "public void Test() { var number = 42; }";

        bool exceptionThrown = false;
        try
        {
            JaySharpCodeValidator.EnforceExplicitVar(sampleCode, config);
        }
        catch (CodeStyleViolationException)
        {
            exceptionThrown = true;
        }

        exceptionThrown.Oughta().Be(true);
    }

    [JayTest(On = Is.On)]
    public static void EnforceExplicitVar_False_DoesNotThrowOnVarDeclaration()
    {
        JaySharpConfig config = new JaySharpConfig { EnforceExplicitVar = false };
        string sampleCode = "public void Test() { var number = 42; }";

        bool exceptionThrown = false;
        try
        {
            JaySharpCodeValidator.EnforceExplicitVar(sampleCode, config);
        }
        catch (CodeStyleViolationException)
        {
            exceptionThrown = true;
        }

        exceptionThrown.Oughta().Be(false);
    }

    [JayTest(On = Is.On)]
    public static void JaySharpRunner_Configure_SetsGlobalConfig()
    {
        JaySharpRunner.Configure(config =>
        {
            config.EnforceExplicitVar = true;
            config.LogLevel = LogLevel.Verbose;
        });

        JaySharpConfig.Current.EnforceExplicitVar.Oughta().Be(true);
        (JaySharpConfig.Current.LogLevel == LogLevel.Verbose).Oughta().Be(true);
    }

    [JayTest(On = Is.On)]
    public static void JaySharpRunner_FluentChaining_AppliesSettings()
    {
        JaySharpRunner.Configure(config => config
            .WithExplicitVar()
            .WithVerboseLogging()
            .WithFileScopedNamespaces()
        );

        JaySharpConfig.Current.EnforceExplicitVar.Oughta().Be(true);
        (JaySharpConfig.Current.LogLevel == LogLevel.Verbose).Oughta().Be(true);
        JaySharpConfig.Current.EnforceFileScopedNamespaces.Oughta().Be(true);
    }
}
