using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.TestRunner;

namespace Tomodachi.Smelly;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   " + Glyphes.Pass("Welcome to Smellydachi (JaySharp)") + "  ");
        Console.WriteLine("========================================");

        SmellyPet pet = new SmellyPet("SmellyTama");
        Console.WriteLine($"Created SmellyPet: {pet.Name} | Hunger: {pet.Hunger}");

        Console.WriteLine("\n" + Glyphes.Pass("Feeding smelly pet..."));
        pet.Feed();
        Console.WriteLine($"Updated Status: Hunger: {pet.Hunger}");

        // Manual configuration fallback example (when config builders, JSON files, or reflection conventions are unavailable)
        JaySharp.Configuration.JaySharpConfig.Current = new JaySharp.Configuration.JaySharpConfig
        {
            EnforceExplicitVar = false,
            LogLevel = LogLevel.Standard,
            RunAllSuitesByDefault = true,
            RunAllTestsByDefault = true
        };

        Console.WriteLine("\n" + Glyphes.Pass("Running JaySharp Test Suite for Smellydachi..."));
        TestSettings.ToTest = typeof(Program).Assembly;
        TestSettings.RunAllSuites = true;
        TestSettings.RunAllTests = true;
        TestRunner.GetAndRunAllTestSuites();

        Console.WriteLine("\n" + Glyphes.Pass("Running JaySharp Junk Test Code Quality Analyzer..."));
        List<JaySharp.Configuration.CodeViolation> violations = JaySharp.Configuration.JaySharpCodeValidator.ValidateDirectory(Directory.GetCurrentDirectory());
        Console.WriteLine(Glyphes.Info($"Detected {violations.Count} test smell violation(s) in sample project:"));
        foreach (JaySharp.Configuration.CodeViolation violation in violations)
        {
            string fileName = Path.GetFileName(violation.FilePath);
            Console.WriteLine("  " + Glyphes.Warning($"[{violation.RuleName}] {fileName}:{violation.LineNumber} -> {violation.LineContent}"));
        }

        Console.WriteLine("\n" + Glyphes.Pass("Smellydachi sample finished."));
    }
}
