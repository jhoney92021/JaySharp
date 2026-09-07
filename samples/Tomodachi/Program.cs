using JaySharp.Shared.Loggers;
using JaySharp.TestSuite.TestRunner;

namespace Tomodachi;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   " + Glyphes.Pass("Welcome to Tomodachi (JaySharp)") + "  ");
        Console.WriteLine("========================================");

        Pet pet = new Pet("Tama");
        Console.WriteLine($"Created Pet: {pet.Name} | Hunger: {pet.Hunger} | Happiness: {pet.Happiness}");

        Console.WriteLine("\n" + Glyphes.Pass("Feeding pet..."));
        pet.Feed();
        Console.WriteLine($"Updated Status: Hunger: {pet.Hunger} | Happiness: {pet.Happiness}");

        Console.WriteLine("\n" + Glyphes.Pass("Running JaySharp Test Suite for Tomodachi..."));
        TestSettings.ToTest = typeof(Program).Assembly;
        TestSettings.RunAllSuites = true;
        TestSettings.RunAllTests = true;
        TestRunner.GetAndRunAllTestSuites();

        Console.WriteLine("\n" + Glyphes.Pass("Tomodachi sample completed successfully!"));
    }

    /// <summary>
    /// JaySharp assembly setup convention method. Automatically discovered by JaySharpConfigLoader.
    /// </summary>
    public static void ConfigureJaySharp(JaySharp.Configuration.JaySharpConfig config)
    {
        config.EnforceFileScopedNamespaces = true;
        config.LogLevel = LogLevel.Verbose;
    }
}
