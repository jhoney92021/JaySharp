using System.Reflection;
using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.TestRunner;

namespace JaySharp;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Contains("--JaySharp") || args.Contains("--JayTest"))
        {
            JayLogger.PrintWithColor(ASCI_Headers.JaySharp_3D, ConsoleColor.Blue);
            JayLogger.PrintWithColor(ASCI_Headers.ByBHiveTechnoligies_JaySharp_3D, ConsoleColor.Red);

            if (args.Contains("-halp"))
            {
                TestSettings.Halp = true;
                foreach (string argument in args)
                {
                    JayLogger.PrintWithColor(Glyphes.Info(argument), ConsoleColor.Red);
                }
            }

            if (args.Contains("-RunTests"))
            {
                if (args.Contains("-AllLogs"))
                {
                    TestSettings.LogLevel = LogLevel.Verbose;
                }

                if (args.Contains("-AllSuites"))
                {
                    TestSettings.RunAllSuites = true;
                }

                if (args.Contains("-AllTests"))
                {
                    TestSettings.RunAllTests = true;
                }

                int featureIdx = Array.IndexOf(args, "-Feature");
                if (featureIdx >= 0 && featureIdx + 1 < args.Length)
                {
                    TestSettings.TargetFeature = args[featureIdx + 1];
                }

                TestSettings.ToTest = typeof(JaySharp.Tests.BooleanTests).Assembly;
                int failedCount = TestRunner.GetAndRunAllTestSuites();
                if (failedCount > 0)
                {
                    Environment.ExitCode = 1;
                }
            }

            if (args.Contains("-Lint"))
            {
                JaySharp.Configuration.JaySharpConfig config = JaySharp.Configuration.JaySharpConfigLoader.LoadConfig();
                if (args.Contains("-ExplicitVar"))
                {
                    config.EnforceExplicitVar = true;
                }

                JayLogger.PrintWithColor("\n" + Glyphes.Pass("Running JaySharp Code Policy Linter..."), ConsoleColor.Cyan);
                JayLogger.PrintWithColor($"  EnforceExplicitVar: {config.EnforceExplicitVar}", ConsoleColor.Gray);

                List<JaySharp.Configuration.CodeViolation> violations = JaySharp.Configuration.JaySharpCodeValidator.ValidateDirectory(Directory.GetCurrentDirectory(), config);

                if (violations.Count > 0)
                {
                    JayLogger.PrintWithColor("\n" + Glyphes.Fail($"Found {violations.Count} code policy violation(s)"), ConsoleColor.Red);
                    foreach (JaySharp.Configuration.CodeViolation violation in violations)
                    {
                        string relativePath = Path.GetRelativePath(Directory.GetCurrentDirectory(), violation.FilePath);
                        JayLogger.PrintWithColor("  " + Glyphes.Fail($"[{violation.RuleName}] {relativePath}:{violation.LineNumber} -> {violation.LineContent}"), ConsoleColor.Yellow);
                    }
                    Environment.ExitCode = 1;
                }
                else
                {
                    JayLogger.PrintWithColor("\n" + Glyphes.Pass("Code policy lint passed cleanly! No violations detected."), ConsoleColor.Green);
                }
                return;
            }

            if (args.Contains("-version"))
            {
                string? versionString = Assembly.GetEntryAssembly()?
                                        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                        .InformationalVersion
                                        .ToString();
                Console.WriteLine(Glyphes.Info($"JaySharp v{versionString}"));
                return;
            }
        }
    }
}